namespace Aqar.Data.Model
{
    public class City:Address
    {
        public int CountryId { get; set; }  
        public Country Country { get; set; }

        public List<Street> Streets { get; set; }

    }
}