using Academia.Domain.Interfaces;
using Academia.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public BancoContext _bancoContext;

        private IStudentRepository? _studentRepository;

        private IEnrollmentRepository? _enrollmentRepository;

        private IPlanRepository? _planRepository ;

        public UnitOfWork(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public IStudentRepository StudentRepository
        {
            get
            {
                return _studentRepository  = _studentRepository?? new StudentRepository(_bancoContext);
            }
        }
        public IEnrollmentRepository EnrollmentRepository
        {
            get
            {
                return _enrollmentRepository = _enrollmentRepository ?? new EnrollmentRepository(_bancoContext);
            }
        }
        public IPlanRepository PlanRepository
        {
            get
            {
                return _planRepository =  _planRepository ?? new PlanRepository(_bancoContext);
            }
        }
        public async Task CommitAsync()
        {
            await _bancoContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _bancoContext.Dispose();
        }
    }
}
