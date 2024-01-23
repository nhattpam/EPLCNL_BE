using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ClassModulesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/[class-modules]")]
    [ApiController]
    public class ClassModulesController : ControllerBase
    {
        private readonly IClassModuleService _classModuleService;

        public ClassModulesController(IClassModuleService classModuleService)
        {
            _classModuleService = classModuleService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ClassModuleResponse>>> GetAllClassModules()
        {
            try
            {
                var rs = await _classModuleService.GetClassModules();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassModuleResponse>> Create([FromBody] ClassModuleRequest request)
        {
            try
            {
                var result = await _classModuleService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ClassModuleResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _classModuleService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ClassModuleResponse>> Update([FromQuery] Guid id, [FromBody] ClassModuleRequest request)
        {
            try
            {
                var rs = await _classModuleService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
