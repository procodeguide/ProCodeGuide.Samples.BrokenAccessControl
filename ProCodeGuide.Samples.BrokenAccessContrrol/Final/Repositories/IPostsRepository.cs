using ProCodeGuide.Samples.BrokenAccessControl.DbEntities;

namespace ProCodeGuide.Samples.BrokenAccessControl.Repositories
{
    public interface IPostsRepository
    {
        string Create(PostEntity post);
        List<PostEntity> GetAll(string CreatedBy);
        PostEntity GetById(int id);
    }
}
