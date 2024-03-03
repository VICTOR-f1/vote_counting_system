using electronic_library_6.Domain.Entities;

namespace electronic_library_6.Domain.Services
{
    public interface IUserService
    {
        Task<bool> IsUserExistsAsync (string username);
        Task<User> RegistrationAsync (string fullname, string username, string password);
        Task<User?> GetUserAsync (string username, string password);
    }
}
