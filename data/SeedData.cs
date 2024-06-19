using HotelBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HotelContext>();
                context.Database.EnsureCreated();

                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                context.Users.AddRange(
                    new User
                    {
                        Username = "admin",
                        Password = "admin",
                        Email = "admin@hotel.com",
                        Role = "Staff",
                        LoyaltyPoints = 0
                    },
                    new User
                    {
                        Username = "guest",
                        Password = "guest",
                        Email = "guest@hotel.com",
                        Role = "Customer",
                        LoyaltyPoints = 100
                    }
                );

                context.Rooms.AddRange(
                    new Room
                    {
                        RoomNumber = "101",
                        Category = RoomCategory.Single
                    },
                    new Room
                    {
                        RoomNumber = "102",
                        Category = RoomCategory.Double
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
