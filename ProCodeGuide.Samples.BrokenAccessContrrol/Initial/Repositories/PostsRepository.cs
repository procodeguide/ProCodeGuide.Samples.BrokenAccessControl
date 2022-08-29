using ProCodeGuide.Samples.BrokenAccessControl.Data;
using ProCodeGuide.Samples.BrokenAccessControl.DbEntities;

namespace ProCodeGuide.Samples.BrokenAccessControl.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private ApplicationDbContext _dbcontext;
        public PostsRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public string Create(PostEntity post)
        {
            _dbcontext.Posts.Add(post);
            _dbcontext.SaveChanges();
            return post.Id.ToString();
        }

        public List<PostEntity> GetAll(string createdBy)
        {
            var posts = _dbcontext.Posts.Where(posts => posts.CreatedBy == createdBy).ToList();
            return posts;
        }

        public PostEntity GetById(int id)
        {
            var post = _dbcontext.Posts.Where(post => post.Id == id).First();
            return post;
        }
    }
}
