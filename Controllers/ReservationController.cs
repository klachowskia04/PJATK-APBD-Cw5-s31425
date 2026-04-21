using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var data = AppData.Reservations.AsQueryable();

        if (date.HasValue)
            data = data.Where(r => r.Date == date.Value);

        if (!string.IsNullOrWhiteSpace(status))
            data = data.Where(r => r.Status.ToLower() == status.ToLower());

        if (roomId.HasValue)
            data = data.Where(r => r.RoomId == roomId.Value);

        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult Add(Reservation reservation)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room == null)
            return BadRequest("Room does not exist");

        if (!room.IsActive)
            return BadRequest("Room is inactive");

        bool conflict = AppData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime
        );

        if (conflict)
            return Conflict("Reservation time conflict");

        reservation.Id = AppData.Reservations.Any()
            ? AppData.Reservations.Max(r => r.Id) + 1
            : 1;

        AppData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Reservation updated)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        reservation.RoomId = updated.RoomId;
        reservation.OrganizerName = updated.OrganizerName;
        reservation.Topic = updated.Topic;
        reservation.Date = updated.Date;
        reservation.StartTime = updated.StartTime;
        reservation.EndTime = updated.EndTime;
        reservation.Status = updated.Status;

        return Ok(reservation);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        AppData.Reservations.Remove(reservation);

        return NoContent();
    }
}