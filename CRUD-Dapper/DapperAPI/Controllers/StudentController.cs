using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto;
using ModelLayer.Responses;
using RepositoryLayer.Entity;

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

    /*[HttpGet]
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
    }*/

    [HttpGet]
    public async Task<IActionResult> GetStudents() //<CreateStudentResponse<List<StudentEntity>>>> GetStudents()
    {
        try
        {
            var students = await _studentBL.GetStudents();

            var response = new StudentResponseModel<List<StudentEntity>>
            {
                Message = "Students retrieved successfully",
                //StatusCode = 200,
                Data = students.ToList()
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = new StudentResponseModel<List<StudentEntity>>
            {
                Success = false,
                Message = ex.Message,
                Data = null // Set Data to null in case of error
            };
            return StatusCode(500, response);
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
                var response = new StudentResponseModel<StudentEntity>
                {
                    Success = false,
                    Message = "Student with provided Id NOT found",
                    Data = null
                };
                return StatusCode(404, response);
            }
            else
            {
                var response = new StudentResponseModel<StudentEntity>
                {
                    Message = "Student found succesfully",
                    //StatusCode = 200,
                    Data = student
                };
                return Ok(response);
            }
        }

        catch (Exception ex)
        {
            var response = new StudentResponseModel<StudentEntity>
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };
            return StatusCode(500, response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertStudent(StudentCreateDto studentDto)
    {
        try
        {
            var createdStudent = await _studentBL.InsertStudent(studentDto);

            var response = new StudentResponseModel<StudentEntity>
            {
                Message = "Student inserted successfully",
                Data = createdStudent
            };
            return CreatedAtRoute("StudentById", new { id = createdStudent.id }, response);
        }
        catch (Exception ex)
        {
            //log error
            //return StatusCode(500, ex.Message);
            var response = new StudentResponseModel<StudentEntity>
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };
            return StatusCode(500, response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, StudentUpdateDto studentDto)
    {
        try
        {
            var student = await _studentBL.GetStudentById(id);
            //if (student == null)
            //    return BadRequest("Student with provided Id NOT found");
            if (student == null)
            {
                var response = new StudentResponseModel<StudentEntity>
                {
                    Success = false,
                    Message = "Student with provided Id NOT found",
                    Data = null
                };
                return NotFound(response);//BadRequest(response);

            }
            else
            {
                await _studentBL.UpdateStudent(id, studentDto);

                /*var response = new StudentResponseModel<StudentEntity>
                {
                    Message = "Student updated successfully",
                    StatusCode = 200,
                    Data = student
                };
                return Ok(response);*/
                /*var response = new StudentResponseModel<StudentEntity>
                {
                    Message = "Student updated successfully",
                    StatusCode = 204, // Use 204 for No Content
                    Data = null // No need to include data if returning NoContent
                };*/
                return NoContent();
            }

        }
        catch (Exception ex)
        {
            //log error
            var response = new StudentResponseModel<StudentEntity>
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };

            return StatusCode(500, response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        try
        {
            var student = await _studentBL.GetStudentById(id);
            //if (student == null)
            //    return BadRequest("Student with provided Id NOT found");
            if (student == null)
            {
                var response = new StudentResponseModel<StudentEntity>
                {
                    Success = false,
                    Message = "Student with provided Id NOT found",
                    Data = null
                };
                return NotFound(response);//BadRequest(response);

            }
            else
            {
                await _studentBL.DeleteStudent(id);
                return NoContent();
            }

        }
        catch (Exception ex)
        {
            //log error
            var response = new StudentResponseModel<StudentEntity>
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };

            return StatusCode(500, response);
        }
    }
}
