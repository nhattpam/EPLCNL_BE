using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.CertificateCoursesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/certificate-courses")]
    [ApiController]
    public class CertificateCoursesController : ControllerBase
    {
        private readonly ICertificateCourseService _certificateCourseService;

        public CertificateCoursesController(ICertificateCourseService certificateCourseService)
        {
            _certificateCourseService = certificateCourseService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CertificateCourseResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CertificateCourseResponse>>> GetAll()
        {
            try
            {
                var rs = await _certificateCourseService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WalletResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WalletResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _certificateCourseService.Get(id);
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
        public async Task<ActionResult<CertificateCourseResponse>> Create([FromBody] CertificateCourseRequest request)
        {
            try
            {
                var result = await _certificateCourseService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CertificateCourseResponse>> Delete(Guid id)
        {
            var rs = await _certificateCourseService.Delete(id);
            return Ok(rs);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CertificateCourseResponse>> Update(Guid id, [FromBody] CertificateCourseRequest request)
        {
            try
            {
                var rs = await _certificateCourseService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
