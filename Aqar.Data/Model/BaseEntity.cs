namespace Aqar.Data.Model
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
    }
}