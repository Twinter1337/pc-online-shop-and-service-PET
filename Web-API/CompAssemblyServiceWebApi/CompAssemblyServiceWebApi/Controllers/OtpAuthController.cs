using ComputerAssemblyServiceBackEnd.Models.Dtos.OtpDtos;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.OtpService;
using Microsoft.AspNetCore.Authorization;

namespace CompAssemblyServiceWebApi.Controllers;

[AllowAnonymous]
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly OtpService _otpService;

    public AuthController(OtpService otpService)
    {
        _otpService = otpService;
    }

    [HttpPost("request-otp")]
    public async Task<IActionResult> RequestOtp([FromBody] OtpRequestDto request)
    {
        try
        {
            var result = await _otpService.SendOtpToEmailAsync(request.Email);
            if (!result)
                return StatusCode(500, "Failed to send OTP.");

            return Ok("OTP sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RequestOtp] Error: {ex.Message}");
            return StatusCode(500, "An unexpected error occurred while sending OTP.");
        }
    }

    [HttpPost("verify-otp")]
    public IActionResult VerifyOtp([FromBody] OtpVerifyDto verifyDto)
    {
        try
        {
            bool isValid = _otpService.VerifyOtp(verifyDto.Email, verifyDto.Code);
            if (!isValid)
                return Unauthorized("Invalid or expired OTP.");

            return Ok("OTP verified successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[VerifyOtp] Error: {ex.Message}");
            return StatusCode(500, "An unexpected error occurred while verifying OTP.");
        }
    }
}