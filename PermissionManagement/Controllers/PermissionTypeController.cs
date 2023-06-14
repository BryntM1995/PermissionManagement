using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Model.Entities;
using PermissionManagement.Service;
using PermissionManagement.Service.DTOs;
using System.Collections.Generic;

namespace PermissionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypeController : ControllerBase
    {
        private readonly IBaseService<PermissionTypeDto> _PermissionTypeService;

        public PermissionTypeController(IBaseService<PermissionTypeDto> permissionTypeService)
        {
            _PermissionTypeService = permissionTypeService;
        }

        [HttpGet]
        public IEnumerable<PermissionTypeDto> GetAll()
        {
            return _PermissionTypeService.GetAll();
        }

        [HttpGet("{id}")]
        public PermissionTypeDto GetById([FromRoute] int id)
        {
            return _PermissionTypeService.GetById(id);
        }

        [HttpPost]
        public IActionResult Add([FromBody] PermissionTypeDto value)
        {
            _PermissionTypeService.Add(value);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] PermissionTypeDto value)
        {
            if (id != value.Id)
                return BadRequest();

            _PermissionTypeService.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            _PermissionTypeService.Remove(id);
            return Ok();
        }
    }
}
