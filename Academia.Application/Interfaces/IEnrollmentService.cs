using Academia.Application.DTOs.Enrollment;
using Academia.Application.DTOs.Student;
using Academia.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task<ResponseEnrollment> CreateEnrollment(CreateEnrollment createEnrollment);
        Task<PagedList<ResponseEnrollment>> GetAll(int currentPage, int pageSize);
        Task<ResponseEnrollment> GetById(int id);
        Task<bool> Remove(int id);
       
    }
}
