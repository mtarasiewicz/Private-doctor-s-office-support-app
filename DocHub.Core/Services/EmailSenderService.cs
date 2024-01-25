using System.Net;
using System.Net.Mail;
using DocHub.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;

namespace DocHub.Core.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IConfiguration _configuration;

    public EmailSenderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var mail = _configuration["EmailSettings:Username"];
        var pw = _configuration["EmailSettings:Password"];
        var client = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, pw)
        };
       
        return client.SendMailAsync(new MailMessage(from: mail, to: email, subject, message));
    }
}