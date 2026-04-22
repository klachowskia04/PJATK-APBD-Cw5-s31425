using System.Runtime.InteropServices.JavaScript;
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

    public static List<Reservation> Reservations = new()
    {
        new()
        {
            Id = 1,
            RoomId = 2,
            OrganizerName = "Anna Kowalczyk",
            Topic = "Data Science Workshop",
            Date = new DateOnly(2026, 4, 21),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "Confirmed"
        },

        new()
        {
            Id = 2,
            RoomId = 1,
            OrganizerName = "Jakub Nowak",
            Topic = "English class",
            Date = new DateOnly(2026, 5, 20),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(10, 30),
            Status = "Planned"
        }
    };
}

