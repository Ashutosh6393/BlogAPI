namespace MegaBlogAPI.DTO
{
    public class AbstractPostDTO
    {
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        public int PostId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public List<CommentDTO> Comments { get; set; } = new();
    }
}


