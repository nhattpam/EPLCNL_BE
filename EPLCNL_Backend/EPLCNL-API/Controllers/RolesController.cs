using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Data;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

      
        [HttpGet]
        public async Task<ActionResult<List<RoleResponse>>> GetAllRoles()
        {
            try
            {
                var rs = await _roleService.GetRoles();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RoleResponse>> Create([FromBody] RoleRequest request)
        {
            try
            {
                var result = await _roleService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<RoleResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _roleService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<RoleResponse>> Update([FromQuery] Guid id, [FromBody] RoleRequest request)
        {
            try
            {
                var rs = await _roleService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
