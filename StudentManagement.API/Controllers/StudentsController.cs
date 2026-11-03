using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]   // => api/students
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// GET api/students  -> read all students
    [HttpGet]
    [ProducesResponseType(typeof(List<StudentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<StudentDto>>> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    /// GET api/students/{studentId}  -> full information of one student
    [HttpGet("{studentId}")]
    [ProducesResponseType(typeof(StudentDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentDetailsDto>> GetById(string studentId)
    {
        var student = await _studentService.GetByIdAsync(studentId);
        if (student is null)
            return NotFound($"Student with ID '{studentId}' was not found.");

        return Ok(student);
    }

    /// POST api/students  -> create a student
    [HttpPost]
    [ProducesResponseType(typeof(StudentDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<StudentDetailsDto>> Create(CreateStudentDto dto)
    {
        var created = await _studentService.CreateAsync(dto);
        if (created is null)
            return Conflict($"A student with ID '{dto.StudentId}' already exists.");

        // 201 Created + a Location header pointing to GET api/students/{id}
        return CreatedAtAction(nameof(GetById), new { studentId = created.StudentId }, created);
    }

    /// PUT api/students/{studentId}  -> edit a student
    [HttpPut("{studentId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string studentId, UpdateStudentDto dto)
    {
        var updated = await _studentService.UpdateAsync(studentId, dto);
        if (!updated)
            return NotFound($"Student with ID '{studentId}' was not found.");

        return NoContent();
    }

    /// DELETE api/students/{studentId}  -> delete a student
    [HttpDelete("{studentId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string studentId)
    {
        var deleted = await _studentService.DeleteAsync(studentId);
        if (!deleted)
            return NotFound($"Student with ID '{studentId}' was not found.");

        return NoContent();
    }
}