namespace HelpPlatform.Core.Contributor.Interfaces;

public interface IEmailSender {
    Task SendEmailAsync(string to, string from, string subject, string body);
}
