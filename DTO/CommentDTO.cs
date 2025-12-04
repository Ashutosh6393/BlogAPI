
using System.ComponentModel.DataAnnotations;

namespace MegaBlogAPI.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public string  CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}