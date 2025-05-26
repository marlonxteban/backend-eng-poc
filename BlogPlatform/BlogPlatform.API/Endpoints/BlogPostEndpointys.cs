using BlogPlatform.Application.DTOs;
using BlogPlatform.Application.UseCases;

namespace BlogPlatform.API.Endpoints
{
    public static class BlogPostEndpointys
    {
        public static void MapBlogPostEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/blogposts", async (
                CreateBlogPostRequest request,
                CreateBlogPostUseCase useCase) =>
            {
                if (string.IsNullOrWhiteSpace(request.Title) ||
                    string.IsNullOrWhiteSpace(request.Content) ||
                    string.IsNullOrWhiteSpace(request.Author))
                {
                    return Results.BadRequest("Title, content, and author are required.");
                }

                var response = await useCase.ExecuteAsync(request);
                return Results.Created($"aoi/blogposts/{response.Id}", response);
            })
                .WithName("CreateBlogPost")
                .WithOpenApi();

            app.MapPatch("/api/blogposts/{id:guid}", async (
                Guid id,
                UpdateBlogPostRequest request,
                UpdateBlogPostUseCase useCase) =>
            {
                if (id != request.Id)
                {
                    return Results.BadRequest("ID in the URL and body must match.");
                }
                if (string.IsNullOrWhiteSpace(request.Title) &&
                    string.IsNullOrWhiteSpace(request.Content))
                {
                    return Results.BadRequest("At least one of title or content must be provided.");
                }

                try
                {
                    await useCase.ExecuteAsync(request);
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            })
                .WithName("PatchBlogPost")
                .WithOpenApi();

            app.MapGet("/api/blogposts", async (
                GetBlogPostUseCases useCases) =>
            {
                var posts = await useCases.GetAllAsync();
                return Results.Ok(posts);
            })
                .WithName("GetAllBlogPosts")
                .WithOpenApi();

            app.Map("/api/blogposts/{id:guid}", async (
                Guid id,
                GetBlogPostUseCases useCases) =>
            {
                var post = await useCases.GetByIdAsync(id);
                return post is null ? Results.NotFound() : Results.Ok(post);
            })
                .WithName("GetPostById")
                .WithOpenApi();

        }
    }
}