namespace HotelBooking.Api.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public Room Room { get; set; }
    }

    public enum BookingStatus
    {
        Booked,
        Cancelled
    }
}
