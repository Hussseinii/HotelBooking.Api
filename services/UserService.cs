using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBooking.Api.Models;
using HotelBooking.Api.Services;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly HotelContext _context;

    public UserService(HotelContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetUsersAsync()
    { 
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        // TODO: Implement logic to create a new user in a data source
        throw new NotImplementedException();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        // TODO: Implement logic to update an existing user in a data source
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        // TODO: Implement logic to delete a user from a data source
        throw new NotImplementedException();
    }
}