using BlogPlatform.Application.DTOs;
using BlogPlatform.Application.UseCases;
using BlogPlatform.Domain.Entities;
using BlogPlatform.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace BlogPlatform.Tests.Application.UseCases
{
    public class CreateBlogPostCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldCreateBlogPost_AndReturnResponse()
        {

            // Arrange
            string fakeTitle = "My Test Title";
            string fakeContent = "My Test Content";
            string fakeAuthor = "Me";
            var request = new CreateBlogPostRequest
            {
                Title = fakeTitle,
                Content = fakeContent,
                Author = fakeAuthor
            };

            var fakePost = new BlogPost(fakeTitle, fakeContent, fakeAuthor);

            var serviceMock = new Mock<IBlogPostService>();
            var repositoryMock = new Mock<IBlogPostRepository>();

            serviceMock
                .Setup(s => s.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(fakePost);

            var useCase = new CreateBlogPostUseCase(repositoryMock.Object, serviceMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(fakeTitle);
            result.Content.Should().Be(fakeContent);
            result.Author.Should().Be(fakeAuthor);

            repositoryMock.Verify(r => r.AddAsync(It.IsAny<BlogPost>()), Times.Once);
        }
    }
}
