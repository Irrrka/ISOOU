namespace ISOOU.Data.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public virtual SystemUser User { get; set; }
    }
}