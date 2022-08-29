namespace ProCodeGuide.Samples.BrokenAccessControl.DbEntities
{
    public class PostEntity
    {
        public int? Id { set; get; }
        public string? Title { set; get; }
        public string? Description { set; get; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
