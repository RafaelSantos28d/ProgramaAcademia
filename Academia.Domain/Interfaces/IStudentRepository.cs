using Academia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student>CreateStudent(Student student);
        Task <IEnumerable<Student>> GetAll();
        Task<Student> GetById(int id);
        Task<bool> Remove(int id);
        Student Update(Student student);
        Task<bool> CpfExist(string cpf);
    }
}
