using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Services.Implements;
using University.BL.Helpers;
using System;

namespace University.Web.Controllers
{
    public class OfficeAssignmentsController : Controller
    {
        private readonly ApiService apiService = new ApiService();

        public async Task<IActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<OfficeAssignmentOutputDTO>>(BL.Helpers.Endpoints.URL_BASE,
                Endpoints.GET_OFFICEASSIGNMENTS,
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

            if (!ModelState.IsValid)
                return View(officeAssignmentDTO);

            try
            {
                var responseDTO = await apiService.RequestAPI<OfficeAssignmentDTO>(BL.Helpers.Endpoints.URL_BASE,
                       Endpoints.POST_COURSES,
                       officeAssignmentDTO,
                       ApiService.Method.Post,
                       false);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(officeAssignmentDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await LoadData();

            var responseDTO = await apiService.RequestAPI<OfficeAssignmentOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.GET_OFFICEASSIGNMENT + id,
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
               Endpoints.PUT_OFFICEASSIGNMENTS + officeAssignmentDTO.InstructorID,
                officeAssignmentDTO,
                ApiService.Method.Put);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(officeAssignmentDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           
            var responseDTO = await apiService.RequestAPI<OfficeAssignmentDTO>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.GET_OFFICEASSIGNMENT + id,
              null,
              ApiService.Method.Get);

            var course = (OfficeAssignmentDTO)responseDTO.Data;

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OfficeAssignmentDTO officeAssignmentDTO)
        {
           
            var responseDTO = await apiService.RequestAPI<OfficeAssignmentDTO>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.DELETE_OFFICEASSIGNMENTS + officeAssignmentDTO.InstructorID,
              null,
              ApiService.Method.Delete);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(officeAssignmentDTO);   
        }

        private async Task LoadData()
        {
            var responseDTO = await apiService.RequestAPI<List<InstructorOutputDTO>>(BL.Helpers.Endpoints.URL_BASE,
               Endpoints.GET_INSTRUCTORS,
                null,
                ApiService.Method.Get);

            var instructors = (List<InstructorOutputDTO>)responseDTO.Data;
            ViewData["instructors"] = new SelectList(instructors, "ID", "FullName");
        }
    }
}
