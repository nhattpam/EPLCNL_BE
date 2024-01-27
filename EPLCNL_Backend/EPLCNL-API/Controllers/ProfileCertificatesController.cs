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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProfileCertificateResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ProfileCertificateResponse>>> GetAll()
        {
            try
            {
                var rs = await _profileCertificateService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfileCertificateResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProfileCertificateResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _profileCertificateService.Get(id);
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
        public async Task<ActionResult<ProfileCertificateResponse>> Create([FromBody] ProfileCertificateRequest request)
        {
            try
            {
                var result = await _profileCertificateService.Create(request);
                return CreatedAtAction(nameof(Create), result);
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
