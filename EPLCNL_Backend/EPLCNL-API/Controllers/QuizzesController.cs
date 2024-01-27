using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.QuizzesService;
using System.Diagnostics;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/quizzes")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuizResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuizResponse>>> GetAll()
        {
            try
            {
                var rs = await _quizService.GetAll();
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
        public async Task<ActionResult<QuizResponse>> Create([FromBody] QuizRequest request)
        {
            try
            {
                var result = await _quizService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("upload-images")]
        public async Task<string> Upload(IFormFile file)
        {
            string link = "";
            if (file != null && file.Length > 0)
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCGiKOtMdILpxGIU5nfbkA_3Af9kNqXIoM"));
                var a = await auth.SignInWithEmailAndPasswordAsync("admin@gmail.com", "admin123");

                var cancellation = new CancellationTokenSource();
                var fileName = file.FileName;
                var stream = file.OpenReadStream();

                var task = new FirebaseStorage(
                    "meowlish-3f184.appspot.com",
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

        [HttpDelete]
        public async Task<ActionResult<QuizResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _quizService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<QuizResponse>> Update([FromQuery] Guid id, [FromBody] QuizRequest request)
        {
            try
            {
                var rs = await _quizService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
