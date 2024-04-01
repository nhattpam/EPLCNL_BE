using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.LessonsService;
using System.Diagnostics;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing lessons.
    /// </summary>
    [Route("api/lessons")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        /// <summary>
        /// Get a list of all lessons.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<LessonResponse>>> GetAll()
        {
            try
            {
                var rs = await _lessonService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get lesson by lesson id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LessonResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LessonResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _lessonService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of materials by lesson id.
        /// </summary>
        [HttpGet("{id}/materials")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaterialResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<MaterialResponse>>> GetAllMaterialsByLesson(Guid id)
        {
            try
            {
                var rs = await _lessonService.GetAllMaterialsByLesson(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new lesson.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LessonResponse>> Create([FromBody] LessonRequest request)
        {
            try
            {
                var result = await _lessonService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete lesson by lesson id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<LessonResponse>> Delete(Guid id)
        {
            var rs = await _lessonService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update lesson by lesson id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<LessonResponse>> Update(Guid id, [FromBody] LessonRequest request)
        {
            try
            {
                var rs = await _lessonService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Upload video of lesson.
        /// </summary>
        [HttpPost("video")]
        public async Task<string> UploadVideo(IFormFile file)
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
                .Child("videos")
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
