using Academia.Domain.Entities;
using Academia.Domain.Enums;
using Academia.Domain.Interfaces;
using Academia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly BancoContext _bancoContext;

        public EnrollmentRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<Enrollment> CreateEnrollment(Enrollment enrollment)
        {
            await _bancoContext.AddAsync(enrollment);
            enrollment.CalculateEndDate(enrollment.Plan.DurationDays);
            return enrollment;
            

        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var enrollments = await _bancoContext.Enrollments.ToListAsync();
            return enrollments;
        }

        public async Task<Enrollment> GetById(int id)
        {
            var enrollment = await _bancoContext.Enrollments.FindAsync(id);
            return enrollment;
        }

        public async Task<bool> Remove(int id)
        {
            var enrollment =await GetById(id);
            if(enrollment == null)
            {
                return false;
            }

            enrollment.Cancel();

            return true;

        }

        public Enrollment Update(Enrollment enrollment)
        {
            _bancoContext.Enrollments.Update(enrollment);
            return enrollment;
        }
    }
}
