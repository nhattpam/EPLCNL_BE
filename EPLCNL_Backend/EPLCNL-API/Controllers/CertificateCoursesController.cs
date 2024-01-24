﻿using Microsoft.AspNetCore.Http;
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

        [HttpDelete]
        public async Task<ActionResult<CertificateCourseResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _certificateCourseService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<CertificateCourseResponse>> Update([FromQuery] Guid id, [FromBody] CertificateCourseRequest request)
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
