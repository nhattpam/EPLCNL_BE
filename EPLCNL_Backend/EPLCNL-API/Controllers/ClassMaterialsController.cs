using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ClassMaterialsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/class-materials")]
    [ApiController]
    public class ClassMaterialsController : ControllerBase
    {
        private readonly IClassMaterialService _ClassMaterialService;

        public ClassMaterialsController(IClassMaterialService ClassMaterialService)
        {
            _ClassMaterialService = ClassMaterialService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ClassMaterialResponse>>> GetAllClassMaterials()
        {
            try
            {
                var rs = await _ClassMaterialService.GetClassMaterials();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassMaterialResponse>> Create([FromBody] ClassMaterialRequest request)
        {
            try
            {
                var result = await _ClassMaterialService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ClassMaterialResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _ClassMaterialService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ClassMaterialResponse>> Update([FromQuery] Guid id, [FromBody] ClassMaterialRequest request)
        {
            try
            {
                var rs = await _ClassMaterialService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
