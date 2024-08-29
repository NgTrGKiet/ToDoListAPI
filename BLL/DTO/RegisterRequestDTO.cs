namespace BLL.DTO
{
    public class RegisterRequestDto
    {
        public RegisterRequestDto(string userName, string password, string name)
        {
            UserName = userName;
            Password = password;
            Name = name;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
