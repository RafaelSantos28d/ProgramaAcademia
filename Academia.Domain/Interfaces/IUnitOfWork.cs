using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        IPlanRepository PlanRepository { get; }
        Task CommitAsync();
    }
}
