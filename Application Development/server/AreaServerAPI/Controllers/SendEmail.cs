using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class SendMail: IReaction
{
    private readonly SmtpClient smtpClient;

    public SendMail(string smtpServer, int port, string username, string password)
    {
        smtpClient = new SmtpClient(smtpServer)
        {
            Port = port,
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential
                            (username, password),

        };
    }

    public void SendEmail(string from, string to, string subject, string body, double temperature)
    {
        try {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from);
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            if (temperature == -200) {
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
            } else
                mailMessage.Body = body + temperature + "°C.\nHave a nice day !";
            try {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
