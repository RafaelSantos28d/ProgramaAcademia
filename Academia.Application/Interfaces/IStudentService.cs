using Academia.Application.DTOs.Student;
using Academia.Domain.Entities;
using Academia.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ResponseStudent> CreateStudent(CreateStudent createStudent);
        Task<PagedList<ResponseStudent>> GetAll(int currentPage, int pageSize);
        Task<ResponseStudent> GetById(int id);
        Task<bool> Remove(int id);
        Task<ResponseStudent> Update(UpdateDTO createStudent);
    }
}
