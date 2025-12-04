namespace MegaBlogAPI.DTO
{

    public class PostResponseDTO
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<CommentDTO> Comments { get; set; } = new();

    }
}