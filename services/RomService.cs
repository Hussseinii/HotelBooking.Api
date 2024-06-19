using HotelBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Services
{
    public class RoomService : IRoomService
    {
        private readonly HotelContext _context;

        public RoomService(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int roomId)
        {
            return await _context.Rooms.FindAsync(roomId);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<bool> DeleteRoomAsync(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null)
            {
                return false;
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
