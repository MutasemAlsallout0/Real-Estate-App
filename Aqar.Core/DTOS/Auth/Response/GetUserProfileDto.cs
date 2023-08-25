namespace Aqar.Core.DTOS.Auth.Response
{
    public class GetUserProfileDto
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserImage { get; set; }
        public string PhoneNumber { get; set; }
        public string OfficeName { get; set; }
 
    }
}