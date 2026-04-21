using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    
// return Ok(); // 200
// return Created(); // 201
// return NoContent(); // 204
// return BadRequest(); // 400
// return NotFound(); // 404
// return Problem(); // 500

    public static List<Student> students =
    [
        new Student
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe"
        },
        new Student
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Doe"
        },
        new Student
        {
            Id = 3,
            FirstName = "John",
            LastName = "Kowalski"
        }
    ]; 

    [HttpGet]
    public IActionResult GetAll([FromQuery] string? lastName)
    {
        return Ok(students.Where(e => lastName is null || e.LastName == lastName).Select(e => new StudentDto
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName
        }));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var student = students.FirstOrDefault(e => e.Id == id);

        if (student is null)
        {
            return NotFound($"Student with id {id} not found");
        }
        
        return Ok(new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName
        });
    }

    [HttpPost]
    public IActionResult Add(CreateStudentDto dto)
    {
        var student = new Student
        {
            Id = students.Max(e => e.Id) + 1,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
        
        students.Add(student);
        
        // return Created($"students/{student.Id}", student);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateStudentDto updateDto)
    {
        var student = students.FirstOrDefault(e => e.Id == id);

        if (student is null)
        {
            return NotFound($"Student with id {id} not found");
        }
        
        student.FirstName = updateDto.FirstName;
        student.LastName = updateDto.LastName;
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var student = students.FirstOrDefault(e => e.Id == id);

        if (student is null)
        {
            return NotFound($"Student with id {id} not found");
        }
        
        students.Remove(student);
        
        return NoContent();
    }
}