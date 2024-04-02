using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.AccountsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;
using Firebase.Auth;
using Firebase.Storage;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net.Mime;
using Service.WalletsService;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing accounts.
    /// </summary>
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Get a list of all accounts.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccountResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AccountResponse>>> GetAll()
        {
            try
            {
                var rs = await _accountService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get account by account id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AccountResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _accountService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Create new account.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountResponse>> Create([FromBody] AccountRequest request)
        {
            try
            {
                var result = await _accountService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Upload image account.
        /// </summary>
        [HttpPost("image")]
        public async Task<string> Upload(IFormFile file)
        {
            string link = "";
            if (file != null && file.Length > 0)
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBik7whdOAZ3MfjE5auYwV-009tXMR2jwM"));
                var a = await auth.SignInWithEmailAndPasswordAsync("admin@gmail.com", "admin123");

                var cancellation = new CancellationTokenSource();
                var fileName = file.FileName;
                var stream = file.OpenReadStream();

                var task = new FirebaseStorage(
                    "meowlish-storage.appspot.com",
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    }
                )
                .Child("images")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);

                try
                {
                    link = await task;
                    Debug.WriteLine(link);
                    return link;
                }
                catch (Exception ex)
                {
                    // Xử lý khi có lỗi xảy ra trong quá trình tải lên
                }

            }
            return link;
        }
        /// <summary>
        /// Delete account by account id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountResponse>> Delete(Guid id)
        {
            var rs = await _accountService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update account by account id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountResponse>> Update(Guid id, [FromBody] AccountRequest request)
        {
            try
            {
                var rs = await _accountService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get learner by account id.
        /// </summary>
        [HttpGet("{id}/learners")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LearnerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LearnerResponse>> GetLearnerByAccount(Guid id)
        {

            try
            {
                var rs = await _accountService.GetLearnerByAccount(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        
        /// <summary>
        /// Get tutor by account id.
        /// </summary>
        [HttpGet("{id}/tutors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TutorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TutorResponse>> GetTutorByAccount(Guid id)
        {

            try
            {
                var rs = await _accountService.GetTutorByAccount(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Get center by account id.
        /// </summary>
        [HttpGet("{id}/centers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CenterResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CenterResponse>> GetCenterByAccount(Guid id)
        {

            try
            {
                var rs = await _accountService.GetCenterByAccount(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Get staff by account id.
        /// </summary>
        [HttpGet("{id}/staffs")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StaffResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StaffResponse>> GetStaffByAccount(Guid id)
        {

            try
            {
                var rs = await _accountService.GetStaffByAccount(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Send mail for successful payment.
        /// </summary>
        [HttpPost("{id}/mail")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountResponse>> SendMail(Guid id, [FromBody] MailRequestModel request)
        {
            var account = await _accountService.Get(id);
            try
            {
                if (account != null)
                {
                    try
                    {
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress("meowlish.company@gmail.com");
                        msg.To.Add(account.Email);
                        msg.Subject = "Payment Successfully!";
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
        <h1>Your Transaction Information</h1>
        <p>Dear {account.FullName},</p>
        {request.Content}
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
                        ntcd.UserName = "meowlish.company@gmail.com";
                        ntcd.Password = "ybpy zzfk taaa glbd"; // Retrieve the password from a secure configuration

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
            return Ok(account);
        } /// <summary>
        /// Send mail for banning account.
        /// </summary>
        [HttpPost("{id}/mail-lock")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountResponse>> SendMailLock(Guid id)
        {
            var account = await _accountService.Get(id);
            try
            {
                if (account != null)
                {
                    try
                    {
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress("meowlish.company@gmail.com");
                        msg.To.Add(account.Email);
                        msg.Subject = "Your account has been banned!";
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
        <h1>Your Account Information</h1>
        <p>Dear {account.FullName},</p>
        <p>Your account has been deactived at {formattedDate}<p/>
        <p>Contact us through meowlish.company@gmail.com if you have any issue!</p>
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
                        ntcd.UserName = "meowlish.company@gmail.com";
                        ntcd.Password = "ybpy zzfk taaa glbd"; // Retrieve the password from a secure configuration

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
            return Ok(account);
        }

        /// <summary>
        /// Get wallet by account id.
        /// </summary>
        [HttpGet("{id}/wallets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WalletResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WalletResponse>> GetWalletByAccount(Guid id)
        {
            try
            {
                var rs = await _accountService.GetWalletByAcount(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
