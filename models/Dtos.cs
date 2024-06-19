using HotelBooking.Api.Models;

namespace HotelBooking.Api.Dtos
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

    }
    public class CreateBooking
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreateBookingByCategory
    {
        public int UserId { get; set; }
        public RoomCategory Category { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
