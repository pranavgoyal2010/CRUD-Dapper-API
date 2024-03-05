using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto;

namespace DapperAPI.Controllers;

[Route("api/students")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentBL _studentBL;
    public StudentController(IStudentBL studentBL)
    {
        _studentBL = studentBL;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        try
        {
            var students = await _studentBL.GetStudents();
            return Ok(students);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "StudentById")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        try
        {
            var student = await _studentBL.GetStudentById(id);

            if (student == null)
            {
                return BadRequest("Student with provided Id NOT found");
            }

            return Ok(student);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertStudent(StudentCreateDto studentDto)
    {
        try
        {
            var createdStudent = await _studentBL.InsertStudent(studentDto);
            return CreatedAtRoute("StudentById", new { id = createdStudent.id }, createdStudent);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, StudentUpdateDto studentDto)
    {
        try
        {
            var student = await _studentBL.GetStudentById(id);
            if (student == null)
                return NotFound();

            await _studentBL.UpdateStudent(id, studentDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
}
