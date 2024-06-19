using DAL.Entities;

namespace DAL.Repository.IRepository
{
    public interface IAuthRepository
    {
        public bool IsUniqueUser(string username);
        Task<string> Login(string Username, string Password);
        Task<User> Register(User user);
    }
}
