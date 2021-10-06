using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApiService apiService = new ApiService();

        public async Task<IActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<CourseOutputDTO>>(BL.Helpers.Endpoints.URL_BASE,
                "api/Courses/GetAll/",
                null,
                ApiService.Method.Get);

            var courses = (List<CourseOutputDTO>)responseDTO.Data;
            return View(courses);
        }
    }
}
