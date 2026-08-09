using Academia.Application.DTOs.Enrollment;
using Academia.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task<ResponseEnrollment> CreateEnrollment(CreateEnrollment createEnrollment);
        Task<IEnumerable<ResponseEnrollment>> GetAll();
        Task<ResponseEnrollment> GetById(int id);
        Task<bool> Remove(int id);
       
    }
}
