using BusinessLayer.Interface;
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
}
