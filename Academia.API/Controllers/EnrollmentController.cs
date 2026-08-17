using Academia.Application.DTOs.Enrollment;
using Academia.Application.Interfaces;
using Academia.Domain.Interfaces;
using Academia.Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }
        
        [HttpGet]
        public async Task<ActionResult<PagedList<ResponseEnrollment>>> GetAll(int currentPage,int pageSize)
        {
            var enrollments = await _enrollmentService.GetAll(currentPage,pageSize);

            return Ok(enrollments);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseEnrollment>> Create(CreateEnrollment createEnrollment)
        {
            var create = await _enrollmentService.CreateEnrollment(createEnrollment);
            return Ok(create);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseEnrollment>> GetById([FromRoute]int id)
        {
            var enrollment = await _enrollmentService.GetById(id);
            return Ok(enrollment);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>>Cancel([FromRoute]int id)
        {
            var result = await _enrollmentService.Cancel(id);
            return Ok(result);
        }
    }
}
