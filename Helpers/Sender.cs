using System.Net.Mail;

namespace Assist_WebConfig.Helpers
{
    public class Sender
    {
        public static void SendEmail(string EmailDestination, string token)
        {
            string urlDomain = Properties.Settings.Default.UrlDomain;

            string EmailSource = Properties.Settings.Default.Email;
            string Pass = Properties.Settings.Default.Password;
            string url = urlDomain + "/User/Recovery/?token=" + token;
            MailMessage oMailMessage = new MailMessage(EmailSource, EmailDestination, "Password recovery",
                "<p>Message for password recovery</p><br>" +
                "<a href='" + url + "'>Click for recover</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient(Properties.Settings.Default.SmtpClient);
            oSmtpClient.EnableSsl = Properties.Settings.Default.EnableSsl;
            oSmtpClient.UseDefaultCredentials = Properties.Settings.Default.DefaultCredentials;
            oSmtpClient.Port = Properties.Settings.Default.Port;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailSource, Pass);

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();
        }
    }
}