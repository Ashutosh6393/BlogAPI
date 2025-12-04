using MegaBlogAPI.DTO;
using MegaBlogAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MegaBlogAPI.Controllers
{



    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private IPostService _postService;
        public PostController(IPostService postservice)
        {
            _postService = postservice;
        }


        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPost()
        {
            var result = await _postService.GetAllPosts();

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var result = await _postService.GetPostById(postId);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateNewPost([FromBody] PostInputDTO postInputDTO)
        {
            var result = await _postService.AddPost(postInputDTO, User);

            if (!result.Success)
            {
                return NotFound();
            }

            return Created("OK", result);
                    
        }

        [Authorize]
        [HttpPatch("update")]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostDTO updatePostDTO)
        {
            var result = await _postService.UpdatePost(updatePostDTO);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Accepted(result);

        }

        [Authorize]
        [HttpDelete("delete/{postId}")]
        public async Task<IActionResult> Delete(int postId)
        {
            var result = await _postService.DeletePost(postId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
