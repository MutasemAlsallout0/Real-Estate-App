namespace Aqar.Data.Model
{
    public class Following
    {
        public string UserId { get; set; }
        public AppUser? User { get; set; }

        public int PublicPageId  { get; set; }

        public PublicPage? PublicPage { get; set; }
        public bool IsFollow { get; set; }


    }
}