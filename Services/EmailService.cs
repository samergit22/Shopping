using HirePlatform.Helpers.Emails;
using HirePlatform.IServices;
using MailKit.Security;
using MimeKit;

namespace HirePlatform.Services
{
    public class EmailService : IEmailServices
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(IEmailStructure emailStructure)
        {

            string body = emailStructure.Body;


            // string body = $"<h2>hello {displayName}</h2><p>your confirm email is<br/>{confirmLink} </p> ";
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_configuration["emadsamer892@gmail.com"]));
            email.To.Add(MailboxAddress.Parse(emailStructure.To));
            email.Subject = emailStructure.Subject;

            // email.Body = new TextPart(TextFormat.Html) { Text = body };
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            using var semtp = new MailKit.Net.Smtp.SmtpClient();
            await semtp.ConnectAsync("smtp.gmail.email", 587, SecureSocketOptions.StartTls);
            await semtp.AuthenticateAsync(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            await semtp.SendAsync(email);
            await semtp.DisconnectAsync(true);

        }

        //public void SendEmail(EmailDTO request)
        //{
        //    var email = new MimeMessage();
        //    email.From.Add(MailboxAddress.Parse("bertha.lowe71@ethereal.email"));
        //    email.To.Add(MailboxAddress.Parse("bertha.lowe71@ethereal.email"));
        //    email.Subject = "The Email Subject";
        //    email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

        //    using var smtp = new SmtpClient();
        //    smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        //    smtp.Authenticate("bertha.lowe71@ethereal.email", "eEHzEpuZHfuYs723dQ");
        //    smtp.Send(email);
        //    smtp.Disconnect(true);
        //}

    }
}
