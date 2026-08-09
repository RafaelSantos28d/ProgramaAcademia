using Academia.Application.DTOs.Enrollment;
using Academia.Application.Interfaces;
using Academia.Domain.Entities;
using Academia.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.Services
{
    public class EnrollmentService :IEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseEnrollment> CreateEnrollment(CreateEnrollment CreateEnrollment)
        {
            var create = _mapper.Map<Enrollment>(CreateEnrollment);
            await _unitOfWork.EnrollmentRepository.CreateEnrollment(create);
            
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ResponseEnrollment>(create);
        }

        public async Task<IEnumerable<ResponseEnrollment>> GetAll()
        {
            var Enrollments = await _unitOfWork.EnrollmentRepository.GetAll();
            return _mapper.Map<IEnumerable<ResponseEnrollment>>(Enrollments);
        }

        public async Task<ResponseEnrollment> GetById(int id)
        {
            var Enrollment = await _unitOfWork.EnrollmentRepository.GetById(id);
            return _mapper.Map<ResponseEnrollment>(Enrollment);
        }

        public async Task<bool> Remove(int id)
        {
            var result = await _unitOfWork.EnrollmentRepository.Remove(id);
            await _unitOfWork.CommitAsync();
            return result;
        }

        
    }
}
