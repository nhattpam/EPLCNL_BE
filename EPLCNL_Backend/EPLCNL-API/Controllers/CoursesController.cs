﻿using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CoursesService;
using System.Diagnostics;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing courses.
    /// </summary>
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Get a list of all courses.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CourseResponse>>> GetAll()
        {
            try
            {
                var rs = await _courseService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get course by course id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CourseResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _courseService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of modules by course id.
        /// </summary>
        [HttpGet("{id}/modules")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModuleResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ModuleResponse>>> GetAllModulesByCourse(Guid id)
        {
            try
            {
                var rs = await _courseService.GetAllModulesByCourse(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of class-modules by course id.
        /// </summary>
        [HttpGet("{id}/class-modules")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassModuleResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ClassModuleResponse>>> GetAllClassModulesByCourse(Guid id)
        {
            try
            {
                var rs = await _courseService.GetAllClassModulesByCourse(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        
        /// <summary>
        /// Get a list of reports by course id.
        /// </summary>
        [HttpGet("{id}/reports")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ReportResponse>>> GetAllReportsByCourse(Guid id)
        {
            try
            {
                var rs = await _courseService.GetAllReportsByCourse(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new course.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CourseResponse>> Create([FromBody] CourseRequest request)
        {
            try
            {
                var result = await _courseService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Upload image of course.
        /// </summary>
        [HttpPost("image")]
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

        /// <summary>
        /// Delete course by course id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseResponse>> Delete(Guid id)
        {
            var rs = await _courseService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update course by course id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseResponse>> Update(Guid id, [FromBody] CourseRequest request)
        {
            try
            {
                var rs = await _courseService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
