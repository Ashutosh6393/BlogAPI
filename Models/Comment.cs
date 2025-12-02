namespace MegaBlogAPI.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public int PostId { get; set; }
        public required Post Post { get; set; }
    }
}
