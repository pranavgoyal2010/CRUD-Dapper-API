using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface;

public interface IStudentRL
{
    public Task<IEnumerable<StudentEntity>> GetStudents();
}
