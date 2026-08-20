using Academia.Application.DTOs.Plan;
using Academia.Application.Interfaces;
using Academia.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin,Employee")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponsePlan>>> GetAll()
        {
            var plan = await _planService.GetAll();
            return Ok(plan);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsePlan>> GetById(int id)
        {
            var plan = await _planService.GetById(id);
            return Ok(plan);
        }
        [HttpPost]
        public async Task<ActionResult<ResponsePlan>> Create(CreatePlan createPlan)
        {
            var create = await _planService.Create(createPlan);
            return Ok(create);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>>Remove([FromRoute]int id)
        {
            var result = await _planService.Remove(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponsePlan>> Update([FromRoute]int id,UpdatePlan updatePlan)
        {
            id = updatePlan.PlanId;
            var update = await _planService.Update(updatePlan);
            return Ok(update);
        }
    }
}
