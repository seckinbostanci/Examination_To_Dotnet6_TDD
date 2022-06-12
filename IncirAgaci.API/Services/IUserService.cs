using IncirAgaci.API.Models;

namespace IncirAgaci.API.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}
