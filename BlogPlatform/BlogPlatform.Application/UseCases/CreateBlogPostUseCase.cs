using BlogPlatform.Application.DTOs;
using BlogPlatform.Domain.Interfaces;

namespace BlogPlatform.Application.UseCases
{
    public class CreateBlogPostUseCase
    {
        private readonly IBlogPostRepository _repository;
        private readonly IBlogPostService _service;

        public CreateBlogPostUseCase(IBlogPostRepository repository, IBlogPostService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<CreateBlogPostResponse> ExecuteAsync(CreateBlogPostRequest request)
        {
            var post = _service.Create(request.Title, request.Content, request.Author);
            if (post.Content == null)
            {
                throw new InvalidOperationException("Blog post content cannot be null.");
            }
            if (post.Title == null)
            {
                throw new InvalidOperationException("Blog post title cannot be null.");
            }

            await _repository.AddAsync(post);

            return new CreateBlogPostResponse
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author = post.Author ?? string.Empty,
                CreatedAt = post.CreatedAt,
            };
        }
    }
}
