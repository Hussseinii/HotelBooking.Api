using HotelBooking.Api.Dtos;
using HotelBooking.Api.Models;
namespace HotelBooking.Api.Mapping
{
    public static class BookingMap
    {
      
        public static BookingDto MapToBookingDto(this Booking booking)
        {
            return new BookingDto
            {
                BookingId = booking.BookingId,
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status.ToString()
            };
        }
    }
}