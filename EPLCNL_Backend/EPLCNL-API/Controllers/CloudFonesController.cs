using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CentersService;
using Service.CloudFoneService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudFonesController : ControllerBase
    {
        private readonly ICloudFoneService _cloudFoneService;

        public CloudFonesController(ICloudFoneService cloudFoneService)
        {
            _cloudFoneService = cloudFoneService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CloudFone>>> GetAll()
        {
            try
            {
                var rs = await _cloudFoneService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostMember([FromBody] CloudFoneRequest memberRequest)
        {
            var _phone = new CloudFone
            {
                ApiKey = memberRequest.ApiKey,
                CallNumber = memberRequest.CallNumber,
                CallName = memberRequest.CallName,
                QueueNumber = memberRequest.QueueNumber,
                ReceiptNumber = memberRequest.ReceiptNumber,
                Key = memberRequest.Key,
                KeyRinging = memberRequest.KeyRinging,
                Status = memberRequest.Status,
                Direction = memberRequest.Direction,
                NumberPbx = memberRequest.NumberPbx,
                Message = memberRequest.Message,
                Data = "",
            };
            _cloudFoneService.Create(_phone);
            return Ok(memberRequest);
        }
    }
}
