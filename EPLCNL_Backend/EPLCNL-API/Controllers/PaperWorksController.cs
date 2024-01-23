using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.PaperWorksService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/paper-works")]
    [ApiController]
    public class PaperWorksController : ControllerBase
    {
        private readonly IPaperWorkService _paperWorkService;

        public PaperWorksController(IPaperWorkService paperWorkService)
        {
            _paperWorkService = paperWorkService;
        }


        [HttpGet]
        public async Task<ActionResult<List<PaperWorkResponse>>> GetAllPaperWorks()
        {
            try
            {
                var rs = await _paperWorkService.GetPaperWorks();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaperWorkResponse>> Create([FromBody] PaperWorkRequest request)
        {
            try
            {
                var result = await _paperWorkService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<PaperWorkResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _paperWorkService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<PaperWorkResponse>> Update([FromQuery] Guid id, [FromBody] PaperWorkRequest request)
        {
            try
            {
                var rs = await _paperWorkService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
