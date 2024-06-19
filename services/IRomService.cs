using HotelBooking.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.Api.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<Room> GetRoomByIdAsync(int roomId);
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(int roomId);
    }
}
