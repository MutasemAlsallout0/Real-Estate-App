namespace Aqar.Data.Model
{
    public class Country:Address
    {
        public List<City> Cities {  get; set; } 
    }
}