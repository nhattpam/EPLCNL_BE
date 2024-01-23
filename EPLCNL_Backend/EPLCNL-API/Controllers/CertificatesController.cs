using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CertificatesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/certificates")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateService _certificateService;

        public CertificatesController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CertificateResponse>>> GetAllCertificates()
        {
            try
            {
                var rs = await _certificateService.GetCertificates();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CertificateResponse>> Create([FromBody] CertificateRequest request)
        {
            try
            {
                var result = await _certificateService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<CertificateResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _certificateService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<CertificateResponse>> Update([FromQuery] Guid id, [FromBody] CertificateRequest request)
        {
            try
            {
                var rs = await _certificateService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
