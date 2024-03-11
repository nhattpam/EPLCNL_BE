using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.MaterialsService;
using System.Diagnostics;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing materials.
    /// </summary>
    [Route("api/materials")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _lessonMaterialService;

        public MaterialsController(IMaterialService lessonMaterialService)
        {
            _lessonMaterialService = lessonMaterialService;
        }

        /// <summary>
        /// Get a list of all materials.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MaterialResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<MaterialResponse>>> GetAll()
        {
            try
            {
                var rs = await _lessonMaterialService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get material by material id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaterialResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MaterialResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _lessonMaterialService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new material.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MaterialResponse>> Create([FromBody] MaterialRequest request)
        {
            try
            {
                var result = await _lessonMaterialService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete material by material id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<MaterialResponse>> Delete(Guid id)
        {
            var rs = await _lessonMaterialService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update material by material id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<MaterialResponse>> Update(Guid id, [FromBody] MaterialRequest request)
        {
            try
            {
                var rs = await _lessonMaterialService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Upload file of material.
        /// </summary>
        [HttpPost("material")]
        public async Task<string> UploadMaterial(IFormFile file)
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
                .Child("materials")
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
