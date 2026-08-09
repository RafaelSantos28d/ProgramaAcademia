using Academia.Application.DTOs.Student;
using Academia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ResponseStudent> CreateStudent(CreateStudent createStudent);
        Task<IEnumerable<ResponseStudent>> GetAll();
        Task<ResponseStudent> GetById(int id);
        Task<bool> Remove(int id);
        Task<ResponseStudent> Update(UpdateDTO createStudent);
    }
}
