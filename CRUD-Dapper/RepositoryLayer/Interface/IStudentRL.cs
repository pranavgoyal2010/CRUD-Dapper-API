﻿using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface;

public interface IStudentRL
{
    public Task<IEnumerable<StudentEntity>> GetStudents();
    public Task<StudentEntity> GetStudentById(int id);
}
