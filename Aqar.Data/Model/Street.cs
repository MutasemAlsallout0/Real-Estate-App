namespace Aqar.Data.Model
{
    public class Street:Address
    {
        public int CityId { get; set; }
        public City City { get; set; }

    }
}