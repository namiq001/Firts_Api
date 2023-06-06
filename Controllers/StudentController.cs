using First_Api.DataContext;
using First_Api.DTOs.StudentDro;
using First_Api.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_Api.Controllers;
[Route("api/Controller")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public StudentController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return StatusCode(200, await _dbContext.Students.ToListAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Message = ex.Message,
            });
        }

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Student? student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
        if(student is null)
        {
            return NotFound();
        }
        return Ok(student);
    }
    [HttpPost]
    public async Task<IActionResult> Create (CreateStudentDto createStudent)
    {
        Student newStudent = new Student
        {
            FullName = createStudent.FullName,
        };
        await _dbContext.Students.AddAsync(newStudent);
        await _dbContext.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Student? student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
        if (student is null)
        {
            return NotFound();
        }
        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateStudentDto updateStudent)
    {
        Student? student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
        if (student is null)
        {
            return NotFound();
        }
        student.FullName = updateStudent.FullName;
        await _dbContext.SaveChangesAsync();
        return Ok(student);
    }
}
