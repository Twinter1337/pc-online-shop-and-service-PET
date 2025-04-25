namespace ComputerAssemblyServiceBackEnd.Models.Dtos.OtpDtos;

public class OtpVerifyDto
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}