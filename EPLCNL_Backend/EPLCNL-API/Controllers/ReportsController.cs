using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ReportsService;
using System.Diagnostics;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    /// <summary>
    /// Controller for managing reports.
    /// </summary>
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Get a list of all reports.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReportResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ReportResponse>>> GetAll()
        {
            try
            {
                var rs = await _reportService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get report by report id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReportResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _reportService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Create new report.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReportResponse>> Create([FromBody] ReportRequest request)
        {
            try
            {
                var result = await _reportService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete report by report id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReportResponse>> Delete(Guid id)
        {
            var rs = await _reportService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update report by report id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ReportResponse>> Update(Guid id, [FromBody] ReportRequest request)
        {
            try
            {
                var rs = await _reportService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
