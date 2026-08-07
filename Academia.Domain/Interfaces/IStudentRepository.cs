using Academia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Interfaces
{
    public interface Student
    {
        Task<Student>CreateStudent(Student Student);
        Task<Student> GetAll();
        Task<Student> GetById(int id);
        Task<Student> Remove(int id);
        Task<Student> Update(Student Student);
    }
}
