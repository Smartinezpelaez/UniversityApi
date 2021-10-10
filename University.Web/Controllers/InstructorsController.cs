using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Services.Implements;


namespace University.Web.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        public async Task<IActionResult> Index()
        {
            var instructorDTO = await apiService.RequestAPI<List<InstructorOutputDTO>>(BL.Helpers.Endpoints.URL_BASE,
                "api/Instructor/GetAll/",
                null,
                ApiService.Method.Get);

            var instructor = (List<InstructorOutputDTO>)instructorDTO.Data;
            return View(instructor);
        }
    }
}
