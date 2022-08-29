namespace ProCodeGuide.Samples.BrokenAccessControl.Models
{
    public class Post
    {
        public int? Id { set; get; }
        public string? Title { set; get; }
        public string? Description { set; get; }
        public DateTime? CreatedOn { get; set; }
    }
}
