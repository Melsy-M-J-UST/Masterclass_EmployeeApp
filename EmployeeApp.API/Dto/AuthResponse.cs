namespace EmployeeApp.API.Dto
{
    public class AuthResponse
    {
        public string accesstoken { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
