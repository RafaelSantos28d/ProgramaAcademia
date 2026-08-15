using Academia.Domain.Entities;
using Academia.Domain.Interfaces;
using Academia.Domain.Pagination;
using Academia.Infrastructure.Context;
using Academia.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly BancoContext _bancoContext;
        
        public StudentRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<Student> CreateStudent(Student student)
        {
            await _bancoContext.Students.AddAsync(student);
            return student;
        }

        public async Task<PagedList<Student>> GetAll(int currentPage,int pageSize)
        {
            var query =  _bancoContext.Students.Include(x=>x.Enrollments).ThenInclude(x=>x.Plan)
                .AsNoTracking();

            return await PaginationHelper.CreateAsync(query, currentPage, pageSize);
        }

        public async Task<Student> GetById(int id)
        {
            var student = await _bancoContext.Students.Include(x=>x.Enrollments).
                FirstOrDefaultAsync(x=>x.StudentId ==id);
            return student;
        }

        public async Task<bool> Remove(int id)
        {
            var student = await GetById(id);
            if(student == null)
            {
                return false;
            }
            _bancoContext.Students.Remove(student);
            return true;
        }

        public Student Update(Student student)
        {
            _bancoContext.Students.Update(student);
            return student;
        }
        public async Task<bool> CpfExist(string cpf)
        {
            return await _bancoContext.Students.AnyAsync(x=>x.CPF == cpf);
        }
    }
}
