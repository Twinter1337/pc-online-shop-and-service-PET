using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Caching.Memory;

namespace ComputerAssemblyServiceBackEnd.OtpService;

public class OtpService
{
    private readonly string _emailFrom;
    private readonly SmtpClient _smtpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly Random _random = new();

    public OtpService(IMemoryCache memoryCache)
    {
        _emailFrom = "testemailforuniversity@gmail.com";
        _memoryCache = memoryCache;

        _smtpClient = new SmtpClient("smtp.gmail.com", 587) 
        {
            Credentials = new NetworkCredential(_emailFrom, "lvhfezyhitzdzcjy"),
            EnableSsl = true
        };
    }

    public async Task<bool> SendOtpToEmailAsync(string emailTo)
    {
        try
        {
            string verificationCode = GenerateVerificationCode();
            
            _memoryCache.Set(emailTo, verificationCode, TimeSpan.FromMinutes(5));

            var message = new MailMessage(_emailFrom, emailTo)
            {
                Subject = "BeePC account verification",
                Body = $"Hi! Your BeePC account verification code is: {verificationCode}"
            };

            await _smtpClient.SendMailAsync(message);
            return true;
        }
        catch (SmtpException ex)
        {
            Console.WriteLine($"SMTP Error: {ex.Message}");
            return false;
        }
    }

    public bool VerifyOtp(string email, string enteredCode)
    {
        if (_memoryCache.TryGetValue(email, out string? codeFromCache))
        {
            if (codeFromCache == enteredCode)
            {
                _memoryCache.Remove(email);
                return true;
            }
        }

        return false;
    }

    private string GenerateVerificationCode()
        => _random.Next(10000, 99999).ToString();
}