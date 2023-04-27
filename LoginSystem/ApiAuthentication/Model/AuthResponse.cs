namespace ApiAuthentication.Model
{
    public class AuthResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
