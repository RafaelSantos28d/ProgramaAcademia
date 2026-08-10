using Academia.Application.DTOs.Enrollment;
using Academia.Application.Interfaces;
using Academia.Domain.Entities;
using Academia.Domain.Enums;
using Academia.Domain.Interfaces;
using Academia.Domain.Validation;
using AutoMapper;

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

        public async Task<ResponseEnrollment> CreateEnrollment(CreateEnrollment createEnrollment)
        {
            var plan = await _unitOfWork.PlanRepository.GetById(createEnrollment.PlanId);
            if (plan == null)
            {
                throw new NotFoundException("Plan not found");
            }
            var student = await _unitOfWork.StudentRepository.GetById(createEnrollment.StudentId);
            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }
            if(student.Enrollments.Any(x=>x.EnrollmentSatatus == EnrollmentSatatus.Active))
            {
                throw new BadRequestException("Student already registered");
            }


            var create = _mapper.Map<Enrollment>(createEnrollment);
            var created = await _unitOfWork.EnrollmentRepository.CreateEnrollment(create);

          
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ResponseEnrollment>(created);
        }

        public async Task<IEnumerable<ResponseEnrollment>> GetAll()
        {
            var Enrollments = await _unitOfWork.EnrollmentRepository.GetAll();
            return _mapper.Map<IEnumerable<ResponseEnrollment>>(Enrollments);
        }

        public async Task<ResponseEnrollment> GetById(int id)
        {
            var enrollment = await _unitOfWork.EnrollmentRepository.GetById(id);
            if(enrollment == null)
            {
                throw new NotFoundException("Enrollment not found");
            }
            return _mapper.Map<ResponseEnrollment>(enrollment);
        }

        public async Task<bool> Remove(int id)
        {
            var enrollment = await _unitOfWork.EnrollmentRepository.GetById(id);
            if (enrollment == null)
            {
                throw new BadRequestException("Enrollment not found");
            }
            var result = await _unitOfWork.EnrollmentRepository.Remove(id);
            await _unitOfWork.CommitAsync();
            return result;
        }

        
    }
}
