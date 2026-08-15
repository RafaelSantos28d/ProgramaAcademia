using Academia.Application.DTOs.Student;
using Academia.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseStudent>> Create(CreateStudent createStudent)
        {
            await _studentService.CreateStudent(createStudent);
            return Ok(createStudent);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseStudent>>> GetAll(int currentPage,int pageSize)
        {
            var students = await _studentService.GetAll(currentPage, pageSize);
            return Ok(students);

        }
        [HttpGet ("{id}")]
        public async Task<ActionResult<IEnumerable<ResponseStudent>>> GetById([FromRoute]int id)
        {
            var student = await _studentService.GetById(id);
            return Ok(student);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>>Remove(int id)
        {
            var result = await _studentService.Remove(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseStudent>> Update([FromRoute] int id,UpdateDTO updateStudent)
        {
            id = updateStudent.StudentId;
            var student = await _studentService.Update(updateStudent);
            return Ok(student);
        }

    }
}
