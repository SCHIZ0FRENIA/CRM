using System.Net.Mail;
using System.Net;

namespace CRM.Core
{
    public static class Mailer
    {
        private static SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("tupa.artyom@gmail.com", "yfhv ggkn fdsg cvfb"),
            EnableSsl = true,
        };

        public static void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                smtpClient.Send("tupa.artyom@gmail.com", recipientEmail, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message + "\n" + recipientEmail);
            }
        }
        public static string SendConfirmationCode(string email, string subject, string before, string after)
        {
            var code = GenerateConfirmationCode();
            SendEmail(email, subject, $"{before} {code} {after}");
            return code;
        }

        private static string GenerateConfirmationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
