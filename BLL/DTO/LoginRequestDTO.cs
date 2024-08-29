namespace BLL.DTO
{
    public class LoginRequestDto
    {
        public LoginRequestDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

