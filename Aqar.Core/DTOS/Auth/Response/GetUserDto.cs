namespace Aqar.Core.DTOS.Auth.Response
{
    public class GetUserDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserImage { get; set; }
        public string UserType { get; set; }
        public int FollowersCount { get; set; }
    }
}