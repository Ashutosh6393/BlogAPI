

using MegaBlogAPI.Data;
using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ReturnTypes;
using MegaBlogAPI.Models;
using MegaBlogAPI.Repository;
using MegaBlogAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace MegaBlogAPI
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository;
        private readonly BlogDbContext _blogDbContext;
        public PostService(IRepository<Post> postRepository, BlogDbContext blogDbContext)
        {
            _postRepository = postRepository;
            _blogDbContext = blogDbContext;
        }
        public async Task<MessageResponse> AddPost(PostInputDTO postInputDTO)
        {
            try
            {
                Post newPost = new Post
                {
                    Title = postInputDTO.Title,
                    Content = postInputDTO.Content,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    UserId = postInputDTO.UserId,
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
                return new PostResponse(Success: true, Message: "Post fetched", post);
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


