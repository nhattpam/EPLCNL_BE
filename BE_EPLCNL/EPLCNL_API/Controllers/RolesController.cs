using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.RolesRepository;

namespace EPLCNL_API.Controllers
{
    [Route("api/Roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepo _roleRepo;
        public RolesController(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Role>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var list = await _roleRepo.GetAll();
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }
    }
}
