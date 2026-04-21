using WebApplication1.Models;

namespace WebApplication1.Data;

public static class AppData
{
    public static List<Room> Rooms = new()
    {
        new()
        {
            Id = 1, Name = "A359", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true
        },
        new()
        {
            Id = 2, Name = "B232", BuildingCode = "B", Floor = 2, Capacity = 30, HasProjector = true, IsActive = true
        },
        new()
        {
            Id = 3, Name = "C101", BuildingCode = "C", Floor = 3, Capacity = 15, HasProjector = false, IsActive = true
        }
    };

    public static List<Reservation> Reservations = new();
}

