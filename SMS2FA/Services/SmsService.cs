using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WebApplication1.Settings;

namespace WebApplication1.Services;

public interface ISmsService
{
    Task SendSmsAsync(string number, string message);
}

public class SmsService(IOptions<TwilioSettings> twilioSettings) : ISmsService
{
    private readonly TwilioSettings _twilioSettings = twilioSettings.Value;
    public async Task SendSmsAsync(string number, string message)
    {
        TwilioClient.Init(_twilioSettings.AccountSId, _twilioSettings.AuthToken);
        await MessageResource.CreateAsync(
            to: number,
            from: _twilioSettings.FromPhoneNumber,
            body: message
        );
    }
}
