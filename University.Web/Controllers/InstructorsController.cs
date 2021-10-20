using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Helpers;
using University.BL.Services.Implements;


namespace University.Web.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        public async Task<IActionResult> Index()
        {
            var instructorDTO = await apiService.RequestAPI<List<InstructorOutputDTO>>(BL.Helpers.Endpoints.URL_BASE,
               Endpoints.GET_INSTRUCTORS,
                null,
                ApiService.Method.Get);

            var instructor = (List<InstructorOutputDTO>)instructorDTO.Data;
            return View(instructor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new InstructorDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstructorDTO instructorDTO)
        {
            var responseDTO = await apiService.RequestAPI<InstructorDTO>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.POST_INSTRUCTORS,
                instructorDTO,
                ApiService.Method.Post);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(instructorDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var responseDTO = await apiService.RequestAPI<InstructorOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.GET_INSTRUCTOR + id,
              null,
              ApiService.Method.Get);

            var instructor = (InstructorOutputDTO)responseDTO.Data;

            return View(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InstructorOutputDTO instructorDTO)
        {
            var responseDTO = await apiService.RequestAPI<InstructorDTO>(BL.Helpers.Endpoints.URL_BASE,
               Endpoints.PUT_INSTRUCTORS + instructorDTO.ID,
                instructorDTO,
                ApiService.Method.Put);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(instructorDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<InstructorOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.GET_INSTRUCTOR + id,
              null,
              ApiService.Method.Get);

            var instructor = (InstructorOutputDTO)responseDTO.Data;

            return View(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(InstructorOutputDTO instructorDTO)
        {
            var responseDTO = await apiService.RequestAPI<InstructorOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.DELETE_INSTRUCTORS + instructorDTO.ID,
              null,
              ApiService.Method.Delete);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(instructorDTO);

        }

        }
}
