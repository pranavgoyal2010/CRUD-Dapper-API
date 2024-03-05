using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;

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


}
