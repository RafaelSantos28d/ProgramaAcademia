using Academia.Application.DTOs.Enrollment;
using Academia.Application.Interfaces;
using Academia.Domain.Entities;
using Academia.Domain.Enums;
using Academia.Domain.Interfaces;
using Academia.Domain.Pagination;
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

        public async Task<PagedList<ResponseEnrollment>> GetAll(int currentPage, int pageSize)
        {
            var enrollments = await _unitOfWork.EnrollmentRepository.GetAll(currentPage, pageSize);

            var enrollmentsResponse =_mapper.Map<IEnumerable<ResponseEnrollment>>(enrollments.Items);
            return new PagedList<ResponseEnrollment>(enrollmentsResponse, currentPage,pageSize,enrollments.TotalCount);
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

        public async Task<bool> Cancel(int id)
        {
            var enrollment = await _unitOfWork.EnrollmentRepository.GetById(id);
            if (enrollment == null)
            {
                throw new BadRequestException("Enrollment not found");
            }
            var result = await _unitOfWork.EnrollmentRepository.Cancel(id);
            await _unitOfWork.CommitAsync();
            return result;
        }

        
    }
}
