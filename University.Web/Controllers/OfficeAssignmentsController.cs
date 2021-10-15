using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class OfficeAssignmentsController : Controller
    {
        private readonly ApiService apiService = new ApiService();

        public async Task<IActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<OfficeAssignmentOutputDTO>>(BL.Helpers.Endpoints.URL_BASE,
                "api/OfficeAssignment/GetAll/",
                null,
                ApiService.Method.Get);
            var office = (List<OfficeAssignmentOutputDTO>)responseDTO.Data;
            return View(office);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadData();
            return View(new OfficeAssignmentDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfficeAssignmentDTO officeAssignmentDTO)
        {
            await LoadData();

            var responseDTO = await apiService.RequestAPI<OfficeAssignmentDTO>(BL.Helpers.Endpoints.URL_BASE,
                "api/OfficeAssignment/",
                officeAssignmentDTO,
                ApiService.Method.Post);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(officeAssignmentDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await LoadData();

            var responseDTO = await apiService.RequestAPI<OfficeAssignmentOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
              "api/OfficeAssignment/GetAll/" + id,
              null,
              ApiService.Method.Get);

            var officeAssignment = (OfficeAssignmentOutputDTO)responseDTO.Data;

            return View(officeAssignment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OfficeAssignmentOutputDTO officeAssignmentDTO)
        {
            await LoadData();

            var responseDTO = await apiService.RequestAPI<OfficeAssignmentDTO>(BL.Helpers.Endpoints.URL_BASE,
                "api/OfficeAssignment/" + officeAssignmentDTO.InstructorID,
                officeAssignmentDTO,
                ApiService.Method.Put);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(officeAssignmentDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await LoadData();
            var responseDTO = await apiService.RequestAPI<OfficeAssignmentDTO>(BL.Helpers.Endpoints.URL_BASE,
              "api/OfficeAssignment/GetAll/" + id,
              null,
              ApiService.Method.Get);

            var course = (OfficeAssignmentDTO)responseDTO.Data;

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OfficeAssignmentDTO officeAssignmentDTO)
        {
            await LoadData();
            var responseDTO = await apiService.RequestAPI<OfficeAssignmentDTO>(BL.Helpers.Endpoints.URL_BASE,
              "api/OfficeAssignment/" + officeAssignmentDTO.InstructorID,
              null,
              ApiService.Method.Delete);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(officeAssignmentDTO);   
        }

        private async Task LoadData()
        {
            var responseDTO = await apiService.RequestAPI<List<InstructorOutputDTO>>(BL.Helpers.Endpoints.URL_BASE,
                "api/Instructor/GetAll/",
                null,
                ApiService.Method.Get);

            var instructors = (List<InstructorOutputDTO>)responseDTO.Data;
            ViewData["instructors"] = new SelectList(instructors, "ID", "FullName");
        }
    }
}
