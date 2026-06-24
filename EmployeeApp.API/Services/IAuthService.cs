using EmployeeApp.Shared.Dto;

namespace EmployeeApp.API.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, string UserId)> Register(RegisterDto request);
        Task<(bool Success, string Message, string token, int ExpiresIn)> Login(LoginDto login);
    }
}
