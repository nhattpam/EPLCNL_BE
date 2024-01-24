using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ClassTypesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/class-types")]
    [ApiController]
    public class ClassTypesController : ControllerBase
    {
        private readonly IClassTypeService _classTypeService;

        public ClassTypesController(IClassTypeService classTypeService)
        {
            _classTypeService = classTypeService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ClassTypeResponse>>> GetAll()
        {
            try
            {
                var rs = await _classTypeService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClassTypeResponse>> Create([FromBody] ClassTypeRequest request)
        {
            try
            {
                var result = await _classTypeService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ClassTypeResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _classTypeService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ClassTypeResponse>> Update([FromQuery] Guid id, [FromBody] ClassTypeRequest request)
        {
            try
            {
                var rs = await _classTypeService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
