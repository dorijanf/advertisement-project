namespace SharedModels.Utils
{
    /// <summary>
    /// Mock email object with the receiver of the email as well as some content
    /// in a string.
    /// </summary>
    public class EmailObject
    {
        public string UserEmail { get; set; }
        public string Content { get; set; }
    }
}
