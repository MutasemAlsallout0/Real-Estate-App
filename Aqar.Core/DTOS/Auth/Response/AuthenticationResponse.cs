namespace Aqar.Core.DTOS.Auth.Response
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}