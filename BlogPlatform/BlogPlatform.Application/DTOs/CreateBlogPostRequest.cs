namespace BlogPlatform.Application.DTOs
{
    public class CreateBlogPostRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}
