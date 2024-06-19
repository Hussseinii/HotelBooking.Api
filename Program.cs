using HotelBooking.Api.Data;
using HotelBooking.Api.Dtos;
using HotelBooking.Api.Models;
using HotelBooking.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HotelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelBookingDbcontext")));

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

SeedData.Initialize(app.Services);

// user  endpoints
app.MapGet("/users", async (IUserService userService) => await userService.GetUsersAsync());
app.MapGet("/users/{id}", async (IUserService userService, int id) =>
    await userService.GetUserByIdAsync(id) is User user ? Results.Ok(user) : Results.NotFound());

//Room endpoints
app.MapGet("/rooms", async (IRoomService roomService) => await roomService.GetRoomsAsync());

app.MapGet("/rooms/{id}", async (IRoomService roomService, int id) =>
    await roomService.GetRoomByIdAsync(id) is Room room ? Results.Ok(room) : Results.NotFound());

app.MapPut("/rooms/{id}", async (IRoomService roomService, int id, Room inputRoom) =>
{
    var room = await roomService.GetRoomByIdAsync(id);
    if (room is null) return Results.NotFound();

    inputRoom.RoomId = id; 
    await roomService.UpdateRoomAsync(inputRoom);
    return Results.NoContent();
});

//Booking endpoints

app.MapGet("/bookings", async (IBookingService bookingService) => await bookingService.GetBookingsAsync());

app.MapGet("/bookings/{id}", async (IBookingService bookingService, int id) =>
    await bookingService.GetBookingByIdAsync(id) is Booking booking ? Results.Ok(booking) : Results.NotFound());

app.MapPost("/bookings", async (IBookingService bookingService, CreateBooking booking) =>
{
    var result  = await bookingService.CreateBookingAsync(booking);
    return Results.Created($"/bookings/{result.BookingId}", booking);
});

app.MapPost("/bookings/category", async (IBookingService bookingService, CreateBookingByCategory booking) =>
{
    var result = await bookingService.CreateBookingByCategoryAsync(booking);
    return Results.Created($"/bookings/{result.BookingId}", booking);
});

app.MapPut("/bookings/{id}", async (IBookingService bookingService, int id, Booking inputBooking) =>
{
    var booking = await bookingService.GetBookingByIdAsync(id);
    if (booking is null) return Results.NotFound();

    inputBooking.BookingId = id; 
    await bookingService.UpdateBookingAsync(inputBooking);
    return Results.NoContent();
});

app.MapDelete("/bookings/{id}", async (IBookingService bookingService, int id) =>
{
    var success = await bookingService.DeleteBookingAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
});

app.Run();
