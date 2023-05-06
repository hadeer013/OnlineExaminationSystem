using System.Net;
using System.Net.Mail;

namespace exam.PL.Helper.Email
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.sendgrid.net", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("apikey", "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");
            client.Send("hadrem605@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
