using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Business.MailServices
{
    public class AccountCreatedMailService
    {
        public void SendMail(string fullName, string email)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("NG-Commerce", "ngcommercee@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(fullName, email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Account Created";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Dear {fullName}, Your account has been created successfully.</p> <p>Thanks for choosing us.</p>";
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
