using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.DTOs
{
    public class UpdateBlogPostRequest
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty!;
        public string? Content { get; set; } = string.Empty;
    }
}
