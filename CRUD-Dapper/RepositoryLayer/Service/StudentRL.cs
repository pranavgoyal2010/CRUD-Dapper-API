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
            bool tableExists = await connection.QueryFirstOrDefaultAsync<bool>
             ("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Students'");

            if (!tableExists)
            {
                // Create table if it doesn't exist
                await connection.ExecuteAsync(@"
                    CREATE TABLE Students (      
                        id int PRIMARY KEY IDENTITY(1,1),     
                        admission_no VARCHAR(45) NOT NULL,  
                        first_name VARCHAR(45) NOT NULL,      
                        last_name VARCHAR(45) NOT NULL,  
                        age int,  
                        city VARCHAR(25) NOT NULL      
                    );");
            }

            var Id = await connection.ExecuteScalarAsync<int>(query, parameters);
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

    public async Task UpdateStudent(int Id, StudentUpdateDto studentDto)
    {
        var query = "UPDATE Students SET admission_no = @admission_no, first_name = @first_name, last_name = @last_name, age = @age, city = @city WHERE id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", Id, DbType.Int32);
        parameters.Add("admission_no", studentDto.admission_no, DbType.String);
        parameters.Add("first_name", studentDto.first_name, DbType.String);
        parameters.Add("last_name", studentDto.last_name, DbType.String);
        parameters.Add("age", studentDto.age, DbType.Int32);
        parameters.Add("city", studentDto.city, DbType.String);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task DeleteStudent(int Id)
    {
        var query = "DELETE FROM Students WHERE id = @Id";
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { Id });
        }
    }
}
