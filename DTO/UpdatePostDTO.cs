

namespace MegaBlogAPI.DTO
{

    public class UpdatePostDTO
    {
        public required int postId { get; set; }
        public required string title { get; set; }
        public required string content { get; set; }
    }
}