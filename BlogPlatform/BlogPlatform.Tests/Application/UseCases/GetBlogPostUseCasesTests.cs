using BlogPlatform.Application.UseCases;
using BlogPlatform.Domain.Entities;
using BlogPlatform.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace BlogPlatform.Tests.Application.UseCases
{
    public class GetBlogPostUseCasesTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfDtos_WhenPostsExist()
        {
            // Arrange

            var posts = new List<BlogPost>
        {
            new("Title 1", "Content 1", "Author 1"),
            new("Title 2", "Content 2", "Author 2")
        };

            var repoMock = new Mock<IBlogPostRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(posts);

            var useCase = new GetBlogPostUseCases(repoMock.Object);

            // Act
            var result = await useCase.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
            result[0].Title.Should().Be("Title 1");
            result[1].Author.Should().Be("Author 2");

            repoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoPostsExist()
        {
            // Arrange
            var repoMock = new Mock<IBlogPostRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<BlogPost>());

            var useCase = new GetBlogPostUseCases(repoMock.Object);

            // Act
            var result = await useCase.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDto_WhenPostExists()
        {
            // Arrange
            var post = new BlogPost("Title X", "Content X", "Author X");

            var repoMock = new Mock<IBlogPostRepository>();
            repoMock.Setup(r => r.GetByIdAsync(post.Id)).ReturnsAsync(post);

            var useCase = new GetBlogPostUseCases(repoMock.Object);

            // Act
            var result = await useCase.GetByIdAsync(post.Id);

            // Assert
            result.Should().NotBeNull();
            result!.Title.Should().Be("Title X");
            result.Author.Should().Be("Author X");

            repoMock.Verify(r => r.GetByIdAsync(post.Id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenPostDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var repoMock = new Mock<IBlogPostRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((BlogPost?)null);

            var useCase = new GetBlogPostUseCases(repoMock.Object);

            // Act
            var result = await useCase.GetByIdAsync(id);

            // Assert
            result.Should().BeNull();
            repoMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }
    }
}
