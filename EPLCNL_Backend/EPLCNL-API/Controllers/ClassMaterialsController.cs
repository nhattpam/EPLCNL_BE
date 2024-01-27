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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassMaterialResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ClassMaterialResponse>>> GetAll()
        {
            try
            {
                var rs = await _ClassMaterialService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassMaterialResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ClassMaterialResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _ClassMaterialService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClassMaterialResponse>> Create([FromBody] ClassMaterialRequest request)
        {
            try
            {
                var result = await _ClassMaterialService.Create(request);
                return CreatedAtAction(nameof(Create), result);
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
