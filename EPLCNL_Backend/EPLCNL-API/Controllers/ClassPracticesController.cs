﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ClassPracticesService;
using Service.ClassPraticesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/class-practices")]
    [ApiController]
    public class ClassPracticesController : ControllerBase
    {
        private readonly IClassPracticeService _classPracticeService;

        public ClassPracticesController(IClassPracticeService classPracticeService)
        {
            _classPracticeService = classPracticeService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassPracticeResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ClassPracticeResponse>>> GetAll()
        {
            try
            {
                var rs = await _classPracticeService.GetAll();
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
        public async Task<ActionResult<ClassPracticeResponse>> Create([FromBody] ClassPracticeRequest request)
        {
            try
            {
                var result = await _classPracticeService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ClassPracticeResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _classPracticeService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ClassPracticeResponse>> Update([FromQuery] Guid id, [FromBody] ClassPracticeRequest request)
        {
            try
            {
                var rs = await _classPracticeService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
