using Academia.Application.DTOs.Student;
using Academia.Application.Interfaces;
using Academia.Domain.Entities;
using Academia.Domain.Enums;
using Academia.Domain.Interfaces;
using Academia.Domain.Pagination;
using Academia.Domain.Validation;
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
            var cpfExist = await _unitOfWork.StudentRepository.CpfExist(create.CPF);
            if (cpfExist == true)
            {
                throw new BadRequestException("Student already registered");
            }

            var created = await _unitOfWork.StudentRepository.CreateStudent(create);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ResponseStudent>(created);
        }

        public async Task<PagedList<ResponseStudent>> GetAll(int currentPage,int pageSize)
        {
            var students = await _unitOfWork.StudentRepository.GetAll(currentPage, pageSize);
            var studentsResponse =  _mapper.Map<IEnumerable<ResponseStudent>>(students.Items);
            return new PagedList<ResponseStudent> (studentsResponse,currentPage,pageSize,students.TotalCount);
        }

        public async Task<ResponseStudent> GetById(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetById(id);
            if(student == null)
            {
                throw new NotFoundException("Student not found");
            }
            return _mapper.Map<ResponseStudent>(student);
        }

        public async Task<bool> Remove(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetById(id);
            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }
            if(student.Enrollments.Any(x=>x.EnrollmentSatatus == EnrollmentSatatus.Active))
            {
                throw new BadRequestException("It’s not possible to remove a student with an active enrollment");
            }
            var result = await _unitOfWork.StudentRepository.Remove(id);
            await _unitOfWork.CommitAsync();
            return result;
        }

        public async Task<ResponseStudent> Update(UpdateDTO update)
        {

            var student = await _unitOfWork.StudentRepository.GetById(update.StudentId);
            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }
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
