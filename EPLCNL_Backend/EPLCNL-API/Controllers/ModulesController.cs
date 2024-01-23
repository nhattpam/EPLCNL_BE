using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ModulesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ModuleResponse>>> GetAllModules()
        {
            try
            {
                var rs = await _moduleService.GetModules();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ModuleResponse>> Create([FromBody] ModuleRequest request)
        {
            try
            {
                var result = await _moduleService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ModuleResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _moduleService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ModuleResponse>> Update([FromQuery] Guid id, [FromBody] ModuleRequest request)
        {
            try
            {
                var rs = await _moduleService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
