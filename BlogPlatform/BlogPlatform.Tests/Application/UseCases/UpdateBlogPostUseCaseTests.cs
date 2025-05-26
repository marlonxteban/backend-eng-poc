using BlogPlatform.Application.DTOs;
using BlogPlatform.Application.UseCases;
using BlogPlatform.Domain.Entities;
using BlogPlatform.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace BlogPlatform.Tests.Application.UseCases
{
    public class UpdateBlogPostUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldUpdatePost_WhenPostExists()
        {
            // Arrange
            string fakeTitle = "Outdated Title";
            string fakeContent = "Outdated Content";
            string fakeAuthor = "Me";

            string updatedTitle = "Not an Outdated Title";
            string updatedContent = "Not an Outdated Content";

            var existingPost = new BlogPost(fakeTitle, fakeContent, fakeAuthor);
            var request = new UpdateBlogPostRequest
            {
                Id = existingPost.Id,
                Title = updatedTitle,
                Content = updatedContent
            };

            var repositoryMock = new Mock<IBlogPostRepository>();
            var serviceMock = new Mock<IBlogPostService>();

            repositoryMock
                .Setup(r => r.GetByIdAsync(request.Id))
                .ReturnsAsync(existingPost);

            var useCase = new UpdateBlogPostUseCase(repositoryMock.Object, serviceMock.Object);

            // Act
            await useCase.ExecuteAsync(request);

            // Assert
            serviceMock.Verify(s => s.Update(existingPost, request.Title!, request.Content!), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(existingPost), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenPostDoesNotExist()
        {
            // Arrange
            var request = new UpdateBlogPostRequest
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Content = "Content"
            };

            var repositoryMock = new Mock<IBlogPostRepository>();
            var serviceMock = new Mock<IBlogPostService>();

            repositoryMock
                .Setup(r => r.GetByIdAsync(request.Id))
                .ReturnsAsync((BlogPost?)null);

            var useCase = new UpdateBlogPostUseCase(repositoryMock.Object, serviceMock.Object);

            // Act
            var act = () => useCase.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Blog post not found.");

            serviceMock.Verify(s => s.Update(It.IsAny<BlogPost>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<BlogPost>()), Times.Never);
        }
    }
}
