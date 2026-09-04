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
        public async Task<bool>CpfIsValid(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());


            if (cpf.Length != 11)

                return false;


            if (cpf.Distinct().Count() == 1)

                return false;



            int soma = 0;

            for (int i = 0; i < 9; i++)

                soma += (cpf[i] - '0') * (10 - i);


            int d1 = (soma * 10) % 11;

            if (d1 == 10) d1 = 0;


            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (cpf[i] - '0') * (11 - i);


            int d2 = (soma * 10) % 11;

            if (d2 == 10) d2 = 0;


            return cpf[9] - '0' == d1 && cpf[10] - '0' == d2;
        }
    }
}
