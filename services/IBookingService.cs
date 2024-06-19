using HotelBooking.Api.Dtos;
using HotelBooking.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.Api.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int bookingId);
        Task<BookingDto> CreateBookingAsync(CreateBooking booking);

        // create booking by room category
        Task<BookingDto> CreateBookingByCategoryAsync(CreateBookingByCategory booking);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int bookingId);
    }
}
