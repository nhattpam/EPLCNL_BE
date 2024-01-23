using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ProfileCertificatesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/profile-certificates")]
    [ApiController]
    public class ProfileCertificatesController : ControllerBase
    {
        private readonly IProfileCertificateService _profileCertificateService;

        public ProfileCertificatesController(IProfileCertificateService profileCertificateService)
        {
            _profileCertificateService = profileCertificateService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProfileCertificateResponse>>> GetAllProfileCertificates()
        {
            try
            {
                var rs = await _profileCertificateService.GetProfileCertificates();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProfileCertificateResponse>> Create([FromBody] ProfileCertificateRequest request)
        {
            try
            {
                var result = await _profileCertificateService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ProfileCertificateResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _profileCertificateService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ProfileCertificateResponse>> Update([FromQuery] Guid id, [FromBody] ProfileCertificateRequest request)
        {
            try
            {
                var rs = await _profileCertificateService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
