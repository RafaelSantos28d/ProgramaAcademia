using Academia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> CreateEnrollment(Enrollment enrollment);
        Task<IEnumerable<Enrollment>> GetAll();
        Task<Enrollment> GetById(int id);
        Task<bool> Remove(int id);
        Enrollment Update(Enrollment enrollment);
    }
}
