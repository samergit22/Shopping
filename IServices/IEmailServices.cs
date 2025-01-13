using HirePlatform.Contracts.Email;
using HirePlatform.Helpers.Emails;

namespace HirePlatform.IServices
{
    public interface IEmailServices
    {
         public Task SendEmail(IEmailStructure emailStructure);
        //void SendEmail(EmailDTO request);
    }
}
