using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.QuestionsService;
using System.Diagnostics;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuestionResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuestionResponse>>> GetAll()
        {
            try
            {
                var rs = await _questionService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<QuestionResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _questionService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/question-answers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionAnswerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuestionAnswerResponse>>> GetAllQuestionAnswersByQuestion(Guid id)
        {
            try
            {
                var rs = await _questionService.GetAllQuestionAnswersByQuestion(id);
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
        public async Task<ActionResult<QuestionResponse>> Create([FromBody] QuestionRequest request)
        {
            try
            {
                var result = await _questionService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionResponse>> Delete(Guid id)
        {
            var rs = await _questionService.Delete(id);
            return Ok(rs);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<QuestionResponse>> Update(Guid id, [FromBody] QuestionRequest request)
        {
            try
            {
                var rs = await _questionService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload-images")]
        public async Task<string> UploadImage(IFormFile file)
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

        [HttpPost("upload-audios")]
        public async Task<string> UploadAudio(IFormFile file)
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
                .Child("audios")
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
    }
}
