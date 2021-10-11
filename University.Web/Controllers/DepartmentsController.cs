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
    }
}
