using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Business.MailServices
{
    public class OrderCompletedMailService
    {
        public void SendMail(string fullName, string email, int orderNo)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("NG-Commerce", "ngcommercee@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(fullName, email);
            mimeMessage.To.Add(mailboxAddressTo);
            
            mimeMessage.Subject = "Order Completed";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Dear {fullName}, Your order has been completed successfully.</p> <p>Click the link for review your order.</p> <p>https://ng-commerce.aheroglu.dev/#/order-detail/{orderNo}</p>";
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
