using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.CertificateCoursesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/[certificate-courses]")]
    [ApiController]
    public class CertificateCoursesController : ControllerBase
    {
        private readonly ICertificateCourseService _certificateCourseService;

        public CertificateCoursesController(ICertificateCourseService certificateCourseService)
        {
            _certificateCourseService = certificateCourseService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CertificateCourseResponse>>> GetAllCertificateCourses()
        {
            try
            {
                var rs = await _certificateCourseService.GetCertificateCourses();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CertificateCourseResponse>> Create([FromBody] CertificateCourseRequest request)
        {
            try
            {
                var result = await _certificateCourseService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
