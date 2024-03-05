using Dapper;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

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
}
