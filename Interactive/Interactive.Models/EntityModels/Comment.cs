namespace Interactive.Models.EntityModels
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual Post Post { get; set; }
    }
}