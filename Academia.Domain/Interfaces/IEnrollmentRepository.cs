using Academia.Domain.Entities;
using Academia.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> CreateEnrollment(Enrollment enrollment);
        Task<PagedList<Enrollment>> GetAll(int pageSize,int pageNumber);
        Task<Enrollment> GetById(int id);
        Task<bool> Cancel(int id);
        Enrollment Update(Enrollment enrollment);
    }
}
