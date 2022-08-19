namespace Database.Entities
{
    public class EmailQueue
    {
        public int Id { get; set; }
        public string SendTo { get; set; }
        public string Content { get; set; }
    }
}
