using Academia.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Entities
{
    public class Student
    {
        
        public Student() { }

        public int StudentId { get; private set; }
        public string Name { get; private set; }   
        public string Email { get; private set; }
        public string CPF { get; private set; }
        
        public string Phone { get; private set; }
        public ICollection<Enrollment>? Enrollments { get; private set; }

        public Student(int studentId, string name, string email, string cpf,string phone)
        {

            Validation(studentId, name, email, cpf, phone);
        }

        public void Validation(int studentId, string name, string email, string cpf,string phone)
        {
            DomainValidationException.When(studentId < 0, "Invalid id");
            DomainValidationException.When(name.Length > 250, "Invalid name size ");
            DomainValidationException.When(string.IsNullOrEmpty(name), "Name is required");
            DomainValidationException.When(string.IsNullOrEmpty(email),"E-mail is required");
            DomainValidationException.When(cpf.Length != 11, "Invalid CPF");
            DomainValidationException.When(string.IsNullOrEmpty(cpf), "CPF is required");
            DomainValidationException.When(email.Length > 270, "Invalid e-mail size");
            DomainValidationException.When(string.IsNullOrEmpty(phone), "Phone number is required");
            DomainValidationException.When(phone.Length > 10, "Invalid phone");
            StudentId = studentId;
            Name = name;
            Email = email;
            CPF = cpf;
            Phone = phone;
        }
        public void AlterarDados(string name, string email,string cpf,string phone)
        {
            Name = name;
            Email = email;
            CPF = cpf;
            Phone = phone;
        }

    }
}
