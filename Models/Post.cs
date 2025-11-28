using System.Xml.Linq;

namespace MegaBlogAPI.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public ICollection<Comment> Comment { get; set; } = new List<Comment>();

    }
}