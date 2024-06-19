namespace HotelBooking.Api.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public RoomCategory Category { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
    public enum RoomCategory
    {
       Single,
       Double
    }
}
