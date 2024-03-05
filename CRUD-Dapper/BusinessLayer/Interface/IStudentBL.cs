using RepositoryLayer.Entity;

namespace BusinessLayer.Interface;

public interface IStudentBL
{
    public Task<IEnumerable<StudentEntity>> GetStudents();
}
