

using MegaBlogAPI.Data;
using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Models;
using MegaBlogAPI.Repository.Interface;
using MegaBlogAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MegaBlogAPI
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly BlogDbContext _blogDbContext;
        public PostService(IPostRepository postRepository, BlogDbContext blogDbContext)
        {
            _postRepository = postRepository;
            _blogDbContext = blogDbContext;
        }
        public async Task<MessageResponse> AddPost(PostInputDTO postInputDTO, ClaimsPrincipal User)
        {
            try
            {

                var email = User.FindFirst("Email")?.Value;
                var userId = int.Parse(User.FindFirst("UserId")?.Value!);


                Post newPost = new Post
                {
                    Title = postInputDTO.Title,
                    Content = postInputDTO.Content,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    UserId = userId,
                };

                await _postRepository.AddAsync(newPost);
                await _blogDbContext.SaveChangesAsync();

                return new MessageResponse(Success: true, Message: "Post Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new MessageResponse(Success: false, Message: "Error adding post");

            }

        }

        public async Task<MessageResponse> DeletePost(int id)
        {
            try
            {
                await _postRepository.DeleteAsync(id);
                await _blogDbContext.SaveChangesAsync();
                return new MessageResponse(true, "Post deleted successfully");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new MessageResponse(true, "Error deleting post");
            }
        }

        async public Task<PostListResponse> GetAllPosts()
        {
            try
            {
                var result = await _postRepository.GetAllAsync();

                //PostResponseDTO postResponseDTO = new PostResponseDTO
                //{
                //    PostId = result.Count().
                //}

                return new PostListResponse(Success: true, Message: "All post fetched", result);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PostListResponse(Success: false, Message: "Error fetching posts", null);
            }

        }

        async public Task<PostResponse> GetPostById(int id)
        {
            try
            {
                var post = await _postRepository.GetByIdAsync(id);


                if (post == null)
                {
                    return new PostResponse(Success: false, Message: "No post found", null);

                }

                PostResponseDTO newPostRes = new PostResponseDTO
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    LastUpdatedAt = post.LastUpdatedAt,
                    UserName = post.User.Name,
                    UserEmail = post.User.Email,
                    Comments = post.Comment.Select(c => new CommentDTO
                    {
                        CommentId = c.CommentId,
                        Text = c.Text,
                        CreatedAt = c.CreatedAt,
                        CreatedBy = c.User.Name

                    }).ToList()
                };

                return new PostResponse(Success: true, Message: "Post fetched", newPostRes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PostResponse(Success: true, Message: ex.Message, null);
            }
        }

        public async Task<MessageResponse> UpdatePost(UpdatePostDTO updatePostDTO)
        {
            try
            {

                var existingPost = await _postRepository.GetByIdAsync(updatePostDTO.postId);
                if (existingPost == null)
                {
                    return new MessageResponse(false, "Post not found");
                }

                existingPost.Title = updatePostDTO.title;
                existingPost.Content = updatePostDTO.content;
                existingPost.LastUpdatedAt = DateTime.UtcNow;

                await _postRepository.UpdateAsync(existingPost);
                await _blogDbContext.SaveChangesAsync();

                return new MessageResponse(true, "Post updated successfully");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new MessageResponse(false, "Error updating post");
            }


        }
    }
}


