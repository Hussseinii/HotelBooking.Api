using System.Runtime.CompilerServices;
using HotelBooking.Api.Dtos;
using HotelBooking.Api.Mapping;
using HotelBooking.Api.Models;
using HotelBooking.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly HotelContext _context;

        public BookingService(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            var result = await _context.Bookings.ToListAsync();

            return result;
        }

        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings.Include(b => b.Room).Include(b => b.User).FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBooking createBooking)
        {
            if (createBooking.StartDate.Date < DateTime.Now.Date)
            {
                throw new InvalidOperationException("The start date should not be less then today.");
            }

            if (createBooking.StartDate.Date >= createBooking.EndDate.Date)
            {
                throw new InvalidOperationException("The start date should be before the end date.");
            }

            bool isRoomAvailable = await _context.Bookings
                .AllAsync(b => b.RoomId == createBooking.RoomId &&
                               ((b.EndDate.Date <= createBooking.StartDate.Date || b.StartDate.Date >= createBooking.EndDate.Date)));

            if (!isRoomAvailable)
            {
                throw new InvalidOperationException("The room is not available for the selected dates.");
            }

            // create the booking

            var newBooking = new Booking
            {
                UserId = createBooking.UserId,
                RoomId = createBooking.RoomId,
                StartDate = createBooking.StartDate,
                EndDate = createBooking.EndDate,
                Status = BookingStatus.Booked,
                CreatedAt = System.DateTime.Now,
                UpdatedAt = System.DateTime.Now
            };

            _context.Bookings.Add(newBooking);
    
            await _context.SaveChangesAsync();

            return newBooking.MapToBookingDto();
        }

        
        public async Task<BookingDto> CreateBookingByCategoryAsync(CreateBookingByCategory createBooking)
        {
            if (createBooking.StartDate.Date < DateTime.Now.Date)
            {
                throw new InvalidOperationException("The start date should not be less then today.");
            }

            if (createBooking.StartDate.Date >= createBooking.EndDate.Date)
            {
                throw new InvalidOperationException("The start date should be before the end date.");
            }

            var room = await _context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Category == createBooking.Category &&
                                          r.Bookings.All(b => b.EndDate.Date <= createBooking.StartDate.Date || b.StartDate.Date >= createBooking.EndDate.Date));

            if (room == null)
            {
                throw new InvalidOperationException("The room category is not available.");
            }

            // create the booking
            var newBooking = new Booking
            {
                UserId = createBooking.UserId,
                RoomId = room.RoomId,
                StartDate = createBooking.StartDate,
                EndDate = createBooking.EndDate,
                Status = BookingStatus.Booked,
                CreatedAt = System.DateTime.Now,
                UpdatedAt = System.DateTime.Now
            };

            _context.Bookings.Add(newBooking);
            await _context.SaveChangesAsync();
            
            return newBooking.MapToBookingDto();

        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                return false;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
