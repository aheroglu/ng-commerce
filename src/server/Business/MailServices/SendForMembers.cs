using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Business.MailServices
{
    public class SendForMembers
    {
        public void SendMail(string userName, string email, string subject, string content)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("NG-Commerce", "ngcommercee@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(userName, email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = $"{subject}";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>{content}</p>";
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
