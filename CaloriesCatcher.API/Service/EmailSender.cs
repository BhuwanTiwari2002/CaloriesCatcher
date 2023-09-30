using System.Net;
using System.Net.Mail;
using AuthApi.API.Models;
using AuthApi.API.Service.IService;
using Microsoft.Extensions.Options;

namespace Auth.API.Service;

public class EmailSender : IEmailSender
{
    private readonly EmailAuthOptions _emailCredentials;
    public EmailSender(IOptions<EmailAuthOptions> emailCredentials)
    {
        _emailCredentials = emailCredentials.Value;
    }
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient(_emailCredentials.SmtpHost, 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_emailCredentials.Email,_emailCredentials.Password)
        };
        return client.SendMailAsync(
            new MailMessage(from: _emailCredentials.Email
                , to: email
                , subject
                , message));
    }
}