using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.PaperWorkTypesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/paper-work-types")]
    [ApiController]
    public class PaperWorkTypesController : ControllerBase
    {
        private readonly IPaperWorkTypeService _paperWorkTypeService;

        public PaperWorkTypesController(IPaperWorkTypeService paperWorkTypeService)
        {
            _paperWorkTypeService = paperWorkTypeService;
        }


        [HttpGet]
        public async Task<ActionResult<List<PaperWorkTypeResponse>>> GetAllPaperWorkTypes()
        {
            try
            {
                var rs = await _paperWorkTypeService.GetPaperWorkTypes();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaperWorkTypeResponse>> Create([FromBody] PaperWorkTypeRequest request)
        {
            try
            {
                var result = await _paperWorkTypeService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<PaperWorkTypeResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _paperWorkTypeService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<PaperWorkTypeResponse>> Update([FromQuery] Guid id, [FromBody] PaperWorkTypeRequest request)
        {
            try
            {
                var rs = await _paperWorkTypeService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
