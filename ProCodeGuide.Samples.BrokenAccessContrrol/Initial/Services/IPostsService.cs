using ProCodeGuide.Samples.BrokenAccessControl.DbEntities;
using ProCodeGuide.Samples.BrokenAccessControl.Models;

namespace ProCodeGuide.Samples.BrokenAccessControl.Services
{
    public interface IPostsService
    {
        string Create(Post post, string createdBy);
        List<Post> GetAll(string CreatedBy);
        Post GetById(int id);
    }
}
