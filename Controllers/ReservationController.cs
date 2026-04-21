using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok(AppData.Reservations);

    [HttpPost]
    public IActionResult Add(Reservation reservation)
    {
        var room = AppData.Rooms.FirstOrDefault(x => x.Id == reservation.RoomId);

        if (room == null)
            return BadRequest("Room does not exist");

        reservation.Id = AppData.Reservations.Count + 1;

        AppData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetAll), reservation);
    }
}