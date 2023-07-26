namespace Aqar.Data.Model
{
    public class City: BaseEntity
    {
        public string Name { get; set; } 
        public int CountryId { get; set; }  
        public Country Country { get; set; }

        public List<Street> Streets { get; set; }

    }
}