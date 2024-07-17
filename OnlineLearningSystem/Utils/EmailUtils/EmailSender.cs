using System.Net.Mail;
using System.Net;

namespace OnlineLearningSystem.Utils.EmailUtils
{
    public class EmailSender
    {
        // Constant value
        private string SENDER_NAME = "Online Learning System";
        private MailServiceAccount mailServiceAccount;
        MailAddress senderAddress;
        SmtpClient smtpClient;

        

        public EmailSender()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            // Bind the configuration to the MailServiceAccount class
            mailServiceAccount = new MailServiceAccount();
            configuration.GetSection("MailServiceAccount").Bind(mailServiceAccount);

            //
            senderAddress = new MailAddress(mailServiceAccount.Email, SENDER_NAME);

            //
            smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mailServiceAccount.Email, mailServiceAccount.Password)
            };
        }

        public System.Threading.Tasks.Task SendEmail(string receiverEmail, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = senderAddress,
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(receiverEmail);

            Console.WriteLine("Send email to " + receiverEmail + " successfully!");
            return smtpClient.SendMailAsync(mailMessage);
        }

        public static void SendEmailMultiThread(string subject, string body, string receiverEmail)
        {
            EmailSender sender = new EmailSender();
            sender.SendEmail(receiverEmail, subject, body).Wait();
        }
    }
}
