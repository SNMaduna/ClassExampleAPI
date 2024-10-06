using Architecture_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architecture_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        

        // Reading all info
        [HttpGet]
        [Route("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var results = await _courseRepository.GetAllCourseAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        // Post method to create a new course 
        [HttpPost]
        [Route("AddCourse")]
        public async Task<IActionResult> AddCourse(Course CourseVM)
        {
            var course = new Course
            {
                Name = CourseVM.Name,
                Description = CourseVM.Description,
                Duration = CourseVM.Duration
            };

            try
            {
                await _courseRepository.AddCourseAsync(course);
                return Ok(course);
            }
            catch (Exception)
            {
                return BadRequest("Invalid Transaction");
            }
        }

        //Get method to get a course based on the Id
        [HttpGet]
        [Route("GetCourse/{courseId}")]
        public async Task<IActionResult> GetCourseAsync(int courseId)
        {
            try
            {
                var results = await _courseRepository.GetCourseAsync(courseId);
                if (results == null)
                    return NotFound("No Course found");
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        //Delete method
        [HttpDelete]
        [Route("RemoveCourse/{courseId}")]
        public async Task<IActionResult> RemoveCourseAsync(int courseId)
        {
            try
            {
                var result = await _courseRepository.RemoveCourseAsync(courseId);
                if (result == null)
                { return NotFound($"Course does not exist. Cannot remove"); }


                
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
            

        }
        
        //The put for updating the record 
        [HttpPut]
        [Route("UpdateCourse/{courseId}")]
        public async Task<IActionResult> UpdateCourse(Course course, int courseId)
        {
            if (courseId != course.CourseId)
                return BadRequest("Id does not match");

            try
            {
                var updateCourse = await _courseRepository.UpdateCourseAsync(course);
                return Ok(updateCourse);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

    }
}
