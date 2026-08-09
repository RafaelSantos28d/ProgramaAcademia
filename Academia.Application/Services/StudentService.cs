using Academia.Application.DTOs.Student;
using Academia.Application.Interfaces;
using Academia.Domain.Entities;
using Academia.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseStudent> CreateStudent(CreateStudent createStudent)
        {
            var create = _mapper.Map<Student>(createStudent);
            await _unitOfWork.StudentRepository.CreateStudent(create);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ResponseStudent>(create);
        }

        public async Task<IEnumerable<ResponseStudent>> GetAll()
        {
            var students = await _unitOfWork.StudentRepository.GetAll();
            return _mapper.Map<IEnumerable<ResponseStudent>>(students);
        }

        public async Task<ResponseStudent> GetById(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetById(id);
            return _mapper.Map<ResponseStudent>(student);
        }

        public async Task<bool> Remove(int id)
        {
            var result = await _unitOfWork.StudentRepository.Remove(id);
            await _unitOfWork.CommitAsync();
            return result;
        }

        public async Task<ResponseStudent> Update(UpdateDTO update)
        {
            var student = await _unitOfWork.StudentRepository.GetById(update.StudentId);
            student.AlterarDados(
                 update.Name,
                 update.Email,
                 update.CPF,
                 update.Phone
                );
           
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ResponseStudent>(student);
        }
    }
}
