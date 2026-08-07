using Academia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> CreateEnrollment(Enrollment enrollment);
        Task<Enrollment> GetAll();
        Task<Enrollment> GetById(int id);
        Task<Enrollment> Remove(int id);
        Task<Enrollment> Update(Enrollment enrollment);
    }
}
