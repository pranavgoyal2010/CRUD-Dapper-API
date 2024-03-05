using Dapper;
using ModelLayer.Dto;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Data;

namespace RepositoryLayer.Service;

public class StudentRL : IStudentRL
{
    private readonly AppDbContext _context;

    public StudentRL(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentEntity>> GetStudents()
    {
        var query = "SELECT * FROM Students";

        using (var connection = _context.CreateConnection())
        {
            var students = await connection.QueryAsync<StudentEntity>(query);
            return students.ToList();
        }
    }
    public async Task<StudentEntity> GetStudentById(int id)
    {
        var query = "SELECT * FROM Students WHERE id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var company = await connection.QuerySingleOrDefaultAsync<StudentEntity>(query, new { id });
            return company;
        }
    }

    public async Task<StudentEntity> InsertStudent(StudentCreateDto studentDto)
    {
        var query = "INSERT INTO Students (admission_no, first_name, last_name, age, city) VALUES " +
            "(@admission_no, @first_name, @last_name, @age, @city);" +
            "SELECT CAST(SCOPE_IDENTITY() as int);";

        var parameters = new DynamicParameters();

        parameters.Add("admission_no", studentDto.admission_no, DbType.String);
        parameters.Add("first_name", studentDto.first_name, DbType.String);
        parameters.Add("last_name", studentDto.last_name, DbType.String);
        parameters.Add("age", studentDto.age, DbType.Int32);
        parameters.Add("city", studentDto.city, DbType.String);

        using (var connection = _context.CreateConnection())
        {
            var Id = await connection.QuerySingleAsync<int>(query, parameters);
            var createdStudent = new StudentEntity
            {
                id = Id,
                admission_no = studentDto.admission_no,
                first_name = studentDto.first_name,
                last_name = studentDto.last_name,
                age = studentDto.age,
                city = studentDto.city
            };


            return createdStudent;
        }

    }
}
