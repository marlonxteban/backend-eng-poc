using BlogPlatform.Application.DTOs;
using BlogPlatform.Domain.Interfaces;

namespace BlogPlatform.Application.UseCases
{
    public class UpdateBlogPostUseCase
    {
        private readonly IBlogPostRepository _repository;
        private readonly IBlogPostService _service;

        public UpdateBlogPostUseCase(IBlogPostRepository repository, IBlogPostService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task ExecuteAsync(UpdateBlogPostRequest request)
        {
            var post = await _repository.GetByIdAsync(request.Id) ?? throw new Exception("Blog post not found.");

            _service.Update(post, request.Title, request.Content);
            await _repository.UpdateAsync(post);
        }
    }
}
