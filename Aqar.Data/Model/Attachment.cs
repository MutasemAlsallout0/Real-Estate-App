namespace Aqar.Data.Model
{
    public class Attachment:BaseEntity
    {
        public string Image{ get; set; }
        public int EstateId { get; set; }
        public Estate Estate { get; set; }
    }
}