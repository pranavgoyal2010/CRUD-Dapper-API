using BusinessLayer.Interface;
using ModelLayer.Dto;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service;

public class StudentBL : IStudentBL
{
    private readonly IStudentRL _studentRL;
    public StudentBL(IStudentRL studentRL)
    {
        _studentRL = studentRL;
    }

    public Task<IEnumerable<StudentEntity>> GetStudents()
    {
        return _studentRL.GetStudents();
    }

    public Task<StudentEntity> GetStudentById(int id)
    {
        return _studentRL.GetStudentById(id);
    }

    public Task<StudentEntity> InsertStudent(StudentCreateDto studentdto)
    {
        return _studentRL.InsertStudent(studentdto);
    }

    public async Task UpdateStudent(int Id, StudentUpdateDto studentDto)
    {
        _studentRL.UpdateStudent(Id, studentDto);
    }

    public async Task DeleteStudent(int Id)
    {
        _studentRL.DeleteStudent(Id);
    }

    public async Task UserRegistrationConsumer(CancellationToken cancellationToken)
    {
        _studentRL.UserRegistrationConsumer(cancellationToken);
    }
}
