using MegaBlogAPI.Data;
using MegaBlogAPI.DTO;
using MegaBlogAPI.DTO.ControllerInputDTO;
using MegaBlogAPI.Models;
using MegaBlogAPI.Repository.Interface;
using MegaBlogAPI.Services.Interface;
using MegaBlogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MegaBlogAPI
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly BlogDbContext _blogDbContext;
        private readonly IUserClaimsService _currentUser;


        public PostService(IPostRepository postRepository, BlogDbContext blogDbContext, IUserClaimsService currentUser)
        {
            _postRepository = postRepository;
            _blogDbContext = blogDbContext;
            _currentUser = currentUser;
        }
        public async Task<PostServiceResponse> AddPost(PostInputDTO postInputDTO)
        {
            try
            {
                Post newPost = new Post
                {
                    Title = postInputDTO.Title,
                    Content = postInputDTO.Content,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    UserId = int.Parse(_currentUser.UserId!),
                };

                await _postRepository.AddAsync(newPost);
                await _blogDbContext.SaveChangesAsync();

                // return new post if needed in future

                return new PostServiceResponse(Success: true, Message: "Post Created", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PostServiceResponse(Success: false, Message: "Error adding post", null);

            }

        }

        public async Task<PostServiceResponse> DeletePost(int id)
        {
            try
            {
                await _postRepository.DeleteAsync(id);
                await _blogDbContext.SaveChangesAsync();
                return new PostServiceResponse(true, "Post deleted successfully", null);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PostServiceResponse(true, "Error deleting post", null);
            }
        }

        async public Task<PostServiceResponse> GetAllPosts()
        {
            try
            {
                var result = await _postRepository.GetAllAsync();

                var posts = result.Select(p => new AbstractPostDTO
                {
                    UserName = p.User?.Name ?? string.Empty,
                    UserEmail = p.User?.Email ?? string.Empty,
                    Title = p.Title ?? string.Empty,
                    Content = p.Content ?? string.Empty,
                    CreatedAt = p.CreatedAt,
                    LastUpdatedAt = p.LastUpdatedAt,
                    PostId = p.PostId,
                    Comments = (p.Comment ?? Enumerable.Empty<Comment>())
                                     .Select(c => new CommentDTO
                                     {
                                         Text = c.Text ?? string.Empty,
                                         UserEmail = c.User?.Email ?? string.Empty,
                                         CommentId = c.CommentId,
                                         CreatedAt = c.CreatedAt,
                                         CreatedBy = c.User?.Name ?? string.Empty
                                     })
                                     .ToList()
                }).ToList();


                return new PostServiceResponse(true, "Posts fetched successfuly", posts); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PostServiceResponse(false, "Error fetching projects", null);
            }
        }

        async public Task<PostServiceResponse> GetPostById(int id)
        {
            try
            {
                var post = await _postRepository.GetByIdAsync(id);


                if (post == null)
                {
                    return new PostServiceResponse(Success: false, Message: "No post found", null);
                }


                var fetchedPost = new AbstractPostDTO
                {
                    UserName = post.User?.Name ?? string.Empty,
                    UserEmail = post.User?.Email ?? string.Empty,
                    Title = post.Title ?? string.Empty,
                    Content = post.Content ?? string.Empty,
                    CreatedAt = post.CreatedAt,
                    LastUpdatedAt = post.LastUpdatedAt,
                    PostId = post.PostId,
                    Comments = (post.Comment ?? Enumerable.Empty<Comment>())
                                .Select(c => new CommentDTO
                                {
                                    Text = c.Text ?? string.Empty,
                                    UserEmail = c.User?.Email ?? string.Empty,
                                    CommentId = c.CommentId,
                                    CreatedAt = c.CreatedAt,
                                    CreatedBy = c.User?.Name ?? string.Empty
                                })
                                .ToList()
                };

                var list = new List<AbstractPostDTO> { fetchedPost };




                return new PostServiceResponse(Success: true, Message: "Post fetched", list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PostServiceResponse(Success: true, Message: ex.Message, null);
            }
        }

        public async Task<PostServiceResponse> GetUserPosts(int id)
        {
            try
            {
                var result = await _postRepository.GetAllUserPosts(id);
                var email = _currentUser.Email;
                var name = _currentUser.Name;

                var posts = result?.Select(p => new AbstractPostDTO
                {
                    UserName = p.User?.Name ?? string.Empty,
                    UserEmail = p.User?.Email ?? string.Empty,
                    Title = p.Title ?? string.Empty,
                    Content = p.Content ?? string.Empty,
                    CreatedAt = p.CreatedAt,
                    LastUpdatedAt = p.LastUpdatedAt,
                    PostId = p.PostId,
                    Comments = (p.Comment ?? Enumerable.Empty<Comment>())
                                    .Select(c => new CommentDTO
                                    {
                                        Text = c.Text ?? string.Empty,
                                        UserEmail = c.User?.Email ?? string.Empty,
                                        CommentId = c.CommentId,
                                        CreatedAt = c.CreatedAt,
                                        CreatedBy = c.User?.Name ?? string.Empty
                                    })
                                    .ToList()
                }).Where(e => e.UserEmail == name).ToList();


                return new PostServiceResponse(true, "Successfully fetched users posts", posts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PostServiceResponse(false, "Error fetching user posts", null);
            }

            
        }

        public async Task<PostServiceResponse> UpdatePost(UpdatePostInputDTO updatePostDTO)
        {
            try
            {
                var existingPost = await _postRepository.GetByIdAsync(updatePostDTO.postId);
                if (existingPost == null)
                {
                    return new PostServiceResponse(false, "Post not found", null);
                }

                existingPost.Title = updatePostDTO.title;
                existingPost.Content = updatePostDTO.content;
                existingPost.LastUpdatedAt = DateTime.UtcNow;

                await _postRepository.UpdateAsync(existingPost);
                await _blogDbContext.SaveChangesAsync();

                return new PostServiceResponse(true, "Post updated successfully", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PostServiceResponse(false, "Error updating post", null);
            }


        }

    }
}


