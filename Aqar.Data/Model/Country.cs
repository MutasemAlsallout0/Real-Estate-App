namespace Aqar.Data.Model
{
    public class Country: BaseEntity
    {
        public string Name { get; set; }
        public List<City> Cities {  get; set; } 
    }
}