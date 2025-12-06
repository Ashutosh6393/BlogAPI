using System.ComponentModel.DataAnnotations;
namespace MegaBlogAPI.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public required string Text { get; set; }
        public required string  CreatedBy { get; set; }
        public required string UserEmail { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}