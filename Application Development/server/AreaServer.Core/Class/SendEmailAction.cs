using Google.Apis.Gmail.v1.Data;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class SendMailAction
{
    private SmtpClient _smtpClient;
    private string _smtpServer = "smtp.outlook.com";
    private int _port = 587;
    //private string _username = "craft.area@outlook.com";
    //private string _password = "CraftArea";
    private string _username = "craft.area2@outlook.com";
    private string _password = "CraftArea";

    public SendMailAction() { }

    public bool SendEmail(string to, string body, string nameArea)
    {
        _smtpClient = new SmtpClient(_smtpServer)
        {
            Port = _port,
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential
                    (_username, _password),

        };
        try {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_username);
            mailMessage.To.Add(to);
            mailMessage.Subject = "CraftArea - AREA " + nameArea;
            mailMessage.Body = body + "\nHave a nice day !";
            try {
                _smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        return true;
    }
}
