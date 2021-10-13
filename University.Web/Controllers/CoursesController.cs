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

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CourseDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseDTO courseDTO)
        {
            var responseDTO = await apiService.RequestAPI<CourseDTO>(BL.Helpers.Endpoints.URL_BASE,
                "api/Courses/",
                courseDTO,
                ApiService.Method.Post);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(courseDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var responseDTO = await apiService.RequestAPI<CourseOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
              "api/Courses/GetById/" + id,
              null,
              ApiService.Method.Get);

            var course = (CourseOutputDTO)responseDTO.Data;

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseOutputDTO courseDTO)
        {
            var responseDTO = await apiService.RequestAPI<CourseDTO>(BL.Helpers.Endpoints.URL_BASE,
                "api/Courses/" + courseDTO.CourseID,
                courseDTO,
                ApiService.Method.Put);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(courseDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<CourseOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
              "api/Courses/" + id,
              null,
              ApiService.Method.Get);

            var course = (CourseOutputDTO)responseDTO.Data;

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CourseOutputDTO courseDTO)
        {
            var responseDTO = await apiService.RequestAPI<CourseOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
              "api/Courses/" + courseDTO.CourseID,
              null,
              ApiService.Method.Delete);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(courseDTO);
        }


    }
}
