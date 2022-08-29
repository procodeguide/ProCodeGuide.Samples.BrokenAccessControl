using ProCodeGuide.Samples.BrokenAccessControl.DbEntities;
using ProCodeGuide.Samples.BrokenAccessControl.Models;
using ProCodeGuide.Samples.BrokenAccessControl.Repositories;

namespace ProCodeGuide.Samples.BrokenAccessControl.Services
{
    public class PostsService : IPostsService
    {
        IPostsRepository? _postRepository = null;

        public PostsService(IPostsRepository postRepository)
        {
            _postRepository = postRepository;
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
            Post post = new Post()
            {
                Id = postEntity.Id,
                Title = postEntity.Title,
                CreatedOn = postEntity.CreatedOn,
                Description = postEntity.Description
            };
            return post;
        }
    }
}
