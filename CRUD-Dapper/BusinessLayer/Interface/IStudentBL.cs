using ModelLayer.Dto;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface;

public interface IStudentBL
{
    public Task<IEnumerable<StudentEntity>> GetStudents();
    public Task<StudentEntity> GetStudentById(int id);
    public Task<StudentEntity> InsertStudent(StudentCreateDto studentDto);
    public Task UpdateStudent(int id, StudentUpdateDto studentDto);
}
