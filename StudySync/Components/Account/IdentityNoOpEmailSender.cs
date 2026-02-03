using Microsoft.AspNetCore.Identity;
using StudySync.Data;

namespace StudySync.Components.Account;

internal sealed class IdentityNoOpEmailSender : IEmailSender<ApplicationUser>
{
    private readonly ILogger<IdentityNoOpEmailSender> _logger;

    public IdentityNoOpEmailSender(ILogger<IdentityNoOpEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        _logger.LogInformation("Confirmation link for {Email}: {ConfirmationLink}", email, confirmationLink);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        _logger.LogInformation("Password reset link for {Email}: {ResetLink}", email, resetLink);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        _logger.LogInformation("Password reset code for {Email}: {ResetCode}", email, resetCode);
        return Task.CompletedTask;
    }
}
