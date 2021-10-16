using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Helpers;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        public async Task<IActionResult> Index()
        {
           
            var responseDTO = await apiService.RequestAPI<List<DepartmentOutputDTO>>(BL.Helpers.Endpoints.URL_BASE,
               Endpoints.GET_DEPARTMENTS,
                null,
                ApiService.Method.Get);

            var departments = (List<DepartmentOutputDTO>)responseDTO.Data;
            return View(departments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadData();
            return View(new DepartmentDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDTO departmentDTO)
        {
            await LoadData();

            if (!ModelState.IsValid)
                return View(departmentDTO);

            try
            {
                var responseDTO = await apiService.RequestAPI<DepartmentDTO>(Endpoints.URL_BASE,
                        Endpoints.POST_DEPARTMENTS,
                        departmentDTO,
                        ApiService.Method.Post,
                        false);

                if (responseDTO.Code == (int)HttpStatusCode.Created)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(departmentDTO);

        }            

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await LoadData();
            var responseDTO = await apiService.RequestAPI<DepartmentOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.GET_DEPARTMENT + id,
              null,
              ApiService.Method.Get);

            var department = (DepartmentOutputDTO)responseDTO.Data;

            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentOutputDTO departmentDTO)
        {
            await LoadData();
            var responseDTO = await apiService.RequestAPI<DepartmentDTO>(BL.Helpers.Endpoints.URL_BASE,
                Endpoints.PUT_DEPARTMENTS + departmentDTO.DepartmentID,
                departmentDTO,
                ApiService.Method.Put);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(departmentDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<DepartmentOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
            Endpoints.GET_DEPARTMENT + id,
              null,
              ApiService.Method.Get);

            var course = (DepartmentOutputDTO)responseDTO.Data;

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentOutputDTO departmentDTO)
        {
            var responseDTO = await apiService.RequestAPI<DepartmentOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
              Endpoints.DELETE_DEPARTMENTS + departmentDTO.DepartmentID,
              null,
              ApiService.Method.Delete);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(departmentDTO);     
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

