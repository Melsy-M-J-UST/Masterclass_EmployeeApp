using EmployeeApp.API.Dto;
using EmployeeApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController(IEmployeeService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]EmployeeDto entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result=await service.AddAsync(entity);
                if (result == null) return NotFound();
                else return CreatedAtAction("GetById", new {result.Id}, result);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody]EmployeeDto entity)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result= await service.UpdateAsync(id, entity);
            if (result == null) return NotFound();
            else return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.DeleteAsync(id);
            if(result == null) return NotFound();
            return Ok(result);
        }

    }
}
