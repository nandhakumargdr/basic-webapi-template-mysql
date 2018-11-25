namespace webapi_basic_mysql.Dtos.Auth
{
    public class UserLoginResponseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}