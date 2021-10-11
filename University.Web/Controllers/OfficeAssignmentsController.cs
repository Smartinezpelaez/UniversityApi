using Microsoft.AspNetCore.Mvc;
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
    }
}
