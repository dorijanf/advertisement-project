namespace Database.Entities
{
    public class Advertisement : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserEmail { get; set; }
        public bool FailedToSync { get; set; }
    }
}
