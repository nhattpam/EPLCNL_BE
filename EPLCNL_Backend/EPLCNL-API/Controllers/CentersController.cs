using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.CentersService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/centers")]
    [ApiController]
    public class CentersController : ControllerBase
    {
        private readonly ICenterService _centerService;

        public CentersController(ICenterService centerService)
        {
            _centerService = centerService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CenterResponse>>> GetAllCenters()
        {
            try
            {
                var rs = await _centerService.GetCenters();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CenterResponse>> Create([FromBody] CenterRequest request)
        {
            try
            {
                var result = await _centerService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<CenterResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _centerService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<CenterResponse>> Update([FromQuery] Guid id, [FromBody] CenterRequest request)
        {
            try
            {
                var rs = await _centerService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
