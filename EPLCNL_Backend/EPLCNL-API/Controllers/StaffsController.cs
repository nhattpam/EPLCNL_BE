using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.StaffsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/staffs")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }


        [HttpGet]
        public async Task<ActionResult<List<StaffResponse>>> GetAllStaffs()
        {
            try
            {
                var rs = await _staffService.GetStaffs();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<StaffResponse>> Create([FromBody] StaffRequest request)
        {
            try
            {
                var result = await _staffService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<StaffResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _staffService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<StaffResponse>> Update([FromQuery] Guid id, [FromBody] StaffRequest request)
        {
            try
            {
                var rs = await _staffService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
