using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok(AppData.Rooms);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(x => x.Id == id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpPost]
    public IActionResult Add(Room room)
    {
        room.Id = AppData.Rooms.Max(x => x.Id) + 1;
        AppData.Rooms.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(x => x.Id == id);
        if (room == null) return NotFound();

        AppData.Rooms.Remove(room);
        return NoContent();
    }
}