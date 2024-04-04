using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.CentersService;
using Service.TutorService;
using System.Net.Mail;
using System.Net.Mime;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing centers.
    /// </summary>
    [Route("api/centers")]
    [ApiController]
    public class CentersController : ControllerBase
    {
        private readonly ICenterService _centerService;
        private readonly ITutorService _tutorService;

        public CentersController(ICenterService centerService, ITutorService tutorService)
        {
            _centerService = centerService;
            _tutorService = tutorService;
        }

        /// <summary>
        /// Get a list of all centers.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CenterResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CenterResponse>>> GetAll()
        {
            try
            {
                var rs = await _centerService.GetAll();
                return CreatedAtAction(nameof(Create), rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get center by center id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CenterResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CenterResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _centerService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of tutors by center id.
        /// </summary>
        [HttpGet("{id}/tutors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TutorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<TutorResponse>>> GetAllTutorsByCenter(Guid id)
        {
            try
            {
                var rs = await _centerService.GetAllTutorsByCenter(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Get total amount by center id.
        /// </summary>
        [HttpGet("{id}/total-amount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<decimal>> GetTotalAmountByCenter(Guid id)
        {
            decimal total = 0;
            try
            {
                var rs = await _centerService.GetAllTutorsByCenter(id);
                foreach (var item in rs)
                {
                    decimal tutor = await _tutorService.GetTotalAmountByTutor(item.Id);
                    total +=  tutor;
                }
                return Ok(total);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new center.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CenterResponse>> Create([FromBody] CenterRequest request)
        {
            try
            {
                var result = await _centerService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete center by center id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CenterResponse>> Delete( Guid id)
        {
            var rs = await _centerService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update center by center id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CenterResponse>> Update(Guid id, [FromBody] CenterRequest request)
        {
            try
            {
                var rs = await _centerService.Update(id, request);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Send mail for approving new centers.
        /// </summary>
        [HttpPost("{id}/mail")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CenterResponse>> SendMail(Guid id)
        {
            var center = await _centerService.Get(id);
            try
            {
                if (center != null)
                {
                    try
                    {
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress("meowlish.work@gmail.com");
                        msg.To.Add(center.Email);
                        msg.Subject = "Center Registration Successfully!";
                        // Set the UTC offset for UTC+7
                        TimeSpan utcOffset = TimeSpan.FromHours(7);

                        // Get the current UTC time
                        DateTime utcNow = DateTime.UtcNow;

                        // Convert the UTC time to UTC+7
                        DateTime localTime = utcNow + utcOffset;

                        string formattedDate = localTime.ToString();

                        string htmlBody = $@"
    <html>
    <body>
        <h1>Center Registration Confirmation</h1>
        <p>Dear {center.Name},</p>
        <p>Thanks for giving time with us.</p>
        <p>You now can access your account at: {formattedDate}</p>
        <p>Email: {center.Email}</p>
        <p>Password: {center.Email}</p>      
        <p>You can change your password later.</p>
        <p>Dear, MeowLish.</p>
    </body>
    </html>";

                        msg.Body = htmlBody;
                        msg.IsBodyHtml = true; // Specify that the body is HTML

                        msg.Body = htmlBody;
                        msg.IsBodyHtml = true; // Specify that the body is HTML

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        System.Net.NetworkCredential ntcd = new System.Net.NetworkCredential();
                        ntcd.UserName = "meowlish.work@gmail.com";
                        ntcd.Password = "llyu mfwz slan gkbs"; // Retrieve the password from a secure configuration

                        smtp.Credentials = ntcd;
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception, e.g., log.Error(ex.Message);
                        // You can also return a 500 Internal Server Error response if email sending fails.
                        return StatusCode(StatusCodes.Status500InternalServerError, "Email sending failed.");
                    }
                }


            }
            catch (DbUpdateException)
            {

            }
            return Ok(center);
        }
    }
}
