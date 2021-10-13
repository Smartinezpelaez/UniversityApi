using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        public async Task<IActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<DepartmentOutputDTO>>(BL.Helpers.Endpoints.URL_BASE,
                "api/Department/GetAll/",
                null,
                ApiService.Method.Get);

            var departments = (List<DepartmentOutputDTO>)responseDTO.Data;
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DepartmentDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDTO departmentDTO)
        {
            var responseDTO = await apiService.RequestAPI<DepartmentDTO>(BL.Helpers.Endpoints.URL_BASE,
                "api/Department/",
                departmentDTO,
                ApiService.Method.Post);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(departmentDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var responseDTO = await apiService.RequestAPI<DepartmentOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
              "api/Department/GetById/" + id,
              null,
              ApiService.Method.Get);

            var department = (DepartmentOutputDTO)responseDTO.Data;

            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentOutputDTO departmentDTO)
        {
            var responseDTO = await apiService.RequestAPI<DepartmentDTO>(BL.Helpers.Endpoints.URL_BASE,
                "api/Department/" + departmentDTO.DepartmentID,
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
              "api/Department/GetById/" + id,
              null,
              ApiService.Method.Get);

            var course = (DepartmentOutputDTO)responseDTO.Data;

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentOutputDTO departmentDTO)
        {
            var responseDTO = await apiService.RequestAPI<DepartmentOutputDTO>(BL.Helpers.Endpoints.URL_BASE,
              "api/Department/" + departmentDTO.DepartmentID,
              null,
              ApiService.Method.Delete);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(departmentDTO);
        }



    }
}
