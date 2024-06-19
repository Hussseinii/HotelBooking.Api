using HotelBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

public class HotelContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
