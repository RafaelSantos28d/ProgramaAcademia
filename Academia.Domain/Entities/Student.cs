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

        public Student(int studentId, string name, string email, string cpf, string phone)
        {

            Validation(studentId, name, email, cpf, phone);
        }

        public void Validation(int studentId, string name, string email, string cpf, string phone)
        {
            DomainValidationException.When(studentId < 0, "Invalid id");
            DomainValidationException.When(name.Length > 250, "Invalid name size ");
            DomainValidationException.When(string.IsNullOrEmpty(name), "Name is required");
            DomainValidationException.When(string.IsNullOrEmpty(email), "E-mail is required");
            DomainValidationException.When(cpf.Length > 20, "Invalid CPF");
            DomainValidationException.When(string.IsNullOrEmpty(cpf), "CPF is required");
            DomainValidationException.When(email.Length > 270, "Invalid e-mail size");
            DomainValidationException.When(string.IsNullOrEmpty(phone), "Phone number is required");
            DomainValidationException.When(phone.Length > 20, "Invalid phone");
            StudentId = studentId;
            Name = name;
            Email = email;
            CPF = FormatarCPF(cpf);
            Phone = FormatarCelular(phone);
        }
        public void AlterarDados(string name, string email, string cpf, string phone)
        {
            Name = name;
            Email = email;
            CPF = cpf;
            Phone = phone;
        }
        private static string FormatarCPF(string cpf)
        {

            if (cpf.Length != 11)

                return cpf;


            return $"{cpf.Substring(0, 3)}." +

            $"{cpf.Substring(3, 3)}." +

            $"{cpf.Substring(6, 3)}-" +

            $"{cpf.Substring(9, 2)}";
        }
        private static string FormatarCelular(string celular)
        {

            if (celular.Length != 11)

                return celular;


            return $"({celular.Substring(0, 2)}) " +

            $"{celular.Substring(2, 1)} " +

            $"{celular.Substring(3, 4)}-" +

            $"{celular.Substring(7, 4)}";

        }
    }
}
