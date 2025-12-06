namespace MegaBlogAPI.DTO.ControllerInputDTO
{

    public class UpdatePostInputDTO
    {
        public required int postId { get; set; }
        public required string title { get; set; }
        public required string content { get; set; }
    }
}