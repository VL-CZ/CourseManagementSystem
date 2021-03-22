using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseTestsController : ControllerBase
    {
        private readonly ICourseTestService courseTestService;

        public CourseTestsController(ICourseTestService courseTestService)
        {
            this.courseTestService = courseTestService;
        }

        /// <summary>
        /// add new test to the given course
        /// </summary>
        /// <param name="testToAdd"></param>
        /// <param name="courseId"></param>
        [HttpPost("{courseId}")]
        public void Add(CourseTestVM testToAdd, int courseId)
        {
            var test = new CourseTest(testToAdd.Topic, testToAdd.Questions, testToAdd.Weight);
            courseTestService.AddToCourse(test, courseId);
        }

        /// <summary>
        /// delete test by its id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            courseTestService.Delete(id);
        }

        /// <summary>
        /// get test by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public CourseTestVM Get(int id)
        {
            var test = courseTestService.GetById(id);
            return new CourseTestVM(id, test.Topic, test.Weight, test.Questions);
        }
    }
}