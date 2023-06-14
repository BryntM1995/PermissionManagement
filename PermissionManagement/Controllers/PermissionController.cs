using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Service.DTOs;
using PermissionManagement.Service;
using System.Collections.Generic;

namespace PermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IBaseService<PermissionDto> _PermissionService;

        public PermissionController(IBaseService<PermissionDto> permissionService)
        {
            _PermissionService = permissionService;
        }

        [HttpGet]
        public IEnumerable<PermissionDto> GetAll()
        {
            return _PermissionService.GetAll();
        }

        [HttpGet("{id}")]
        public PermissionDto GetById([FromRoute] int id)
        {
            return _PermissionService.GetById(id);
        }

        [HttpPost]
        public IActionResult Add([FromBody] PermissionDto value)
        {
            _PermissionService.Add(value);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] PermissionDto value)
        {
            if (id != value.Id)
                return BadRequest();

            _PermissionService.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            _PermissionService.Remove(id);
            return Ok();
        }
    }
}
