using Aqar.Core.DTOS.Auth.Request;

namespace Aqar.Infrastructure.HelperServices.EmailHelper
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}