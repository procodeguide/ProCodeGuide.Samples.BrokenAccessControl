using Microsoft.AspNetCore.Authorization;
using ProCodeGuide.Samples.BrokenAccessControl.DbEntities;
using ProCodeGuide.Samples.BrokenAccessControl.Models;
using ProCodeGuide.Samples.BrokenAccessControl.Repositories;

namespace ProCodeGuide.Samples.BrokenAccessControl.Services
{
    public class PostsService : IPostsService
    {
        IPostsRepository? _postRepository = null;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostsService(IPostsRepository postRepository, IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Create(Post post, string createdBy)
        {
            PostEntity postEntity = new PostEntity
            {
                //Id = post.Id,
                Title = post.Title,
                CreatedBy = createdBy,
                CreatedOn = DateTime.Now,
                Description = post.Description
            };
            return _postRepository.Create(postEntity);
        }

        public List<Post> GetAll(string CreatedBy)
        {
            List<Post> posts = new List<Post>();
            foreach(PostEntity postEntity in _postRepository.GetAll(CreatedBy))
            {
                Post post = new Post()
                {
                    Id = postEntity.Id,
                    Title = postEntity.Title,
                    CreatedOn = postEntity.CreatedOn,
                    Description = postEntity.Description
                };
                posts.Add(post);
            }
            return posts;
        }

        public Post GetById(int id)
        {
            PostEntity postEntity = _postRepository.GetById(id);

            var authorizationResult = _authorizationService.AuthorizeAsync
                (_httpContextAccessor.HttpContext.User, postEntity, "IsPostOwnerPolicy");

            Post post = new Post();

            if (authorizationResult.Result.Succeeded)
            {
                post.Id = postEntity.Id;
                post.Title = postEntity.Title;
                post.CreatedOn = postEntity.CreatedOn;
                post.Description = postEntity.Description;
            }

            return post;
        }
    }
}
