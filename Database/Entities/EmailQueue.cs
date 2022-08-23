namespace Database.Entities
{
    public class EmailQueue : BaseEntity
    {
        public int Id { get; set; }
        public string SendTo { get; set; }
        public string Content { get; set; }
    }
}
