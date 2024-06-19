using HotelBooking.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
    }
}
