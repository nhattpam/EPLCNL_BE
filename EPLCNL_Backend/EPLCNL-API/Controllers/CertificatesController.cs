using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CertificatesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing certificates.
    /// </summary>
    [Route("api/certificates")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateService _certificateService;

        public CertificatesController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        /// <summary>
        /// Get a list of all certificates.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CertificateResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CertificateResponse>>> GetAll()
        {
            try
            {
                var rs = await _certificateService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get certificate by certificate id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CertificateResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CertificateResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _certificateService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new certificate.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CertificateResponse>> Create([FromBody] CertificateRequest request)
        {
            try
            {
                var result = await _certificateService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete certificate by certificate id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CertificateResponse>> Delete(Guid id)
        {
            var rs = await _certificateService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update certificate by certificate id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CertificateResponse>> Update(Guid id, [FromBody] CertificateRequest request)
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
