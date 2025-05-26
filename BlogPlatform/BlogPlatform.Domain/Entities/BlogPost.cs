using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPlatform.Domain.Entities
{
    [Table("BlogPosts")]
    public class BlogPost
    {
        [Key]
        public Guid Id { get; private set; }

        [Required]
        [MaxLength(200)]
        public string? Title { get; private set; } = null;

        public string? Content { get; private set; } = null;

        public string? Author { get; private set; } = null;

        [Required]
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private BlogPost() { }

        public BlogPost(string title, string content, string author)
        {
            Id = Guid.NewGuid();
            Title = title;
            Content = content;
            Author = author;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string? title, string? content)
        {
            Title = title ?? Title;
            Content = content ?? Content;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
