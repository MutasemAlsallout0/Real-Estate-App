namespace Aqar.Data.Model
{
    public class PublicPage : BaseEntity
    {
        public string Name { get; set; }    
        public bool ActiveState { get; set; }
        public string UserId { get; set; }
        public AppUser? User { get; set; }
        public List<Following> Followings { get; set; }

    }
}