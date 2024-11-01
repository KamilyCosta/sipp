using Microsoft.AspNetCore.Identity.UI.Services;

namespace SIPP.Util
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask; // Não faz nada
        }
    }
}
