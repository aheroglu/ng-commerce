
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Business.MailServices
{
    public class ForgotPasswordMailService
    {
        public void SendMail(string email)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("NG-Commerce", "ngcommercee@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(string.Empty, email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Reset Password";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Click the link below to reset your password:</p> <p><a href='https://ng-commerce.aheroglu.dev/#/reset-password/{email}'</a>Reset Password</p>";
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("ngcommercee@gmail.com", "guuksybjjuqszyqs");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }
    }
}
