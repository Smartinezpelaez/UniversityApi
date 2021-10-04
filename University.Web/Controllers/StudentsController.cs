using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApiService apiService = new ApiService();

        public async Task <IActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<StudentOutputDTO>>("https://universityapisteven.azurewebsites.net/",
                "api/Students/GetAll/",
                null,
                ApiService.Method.Get,
                false);
        

            var students = (List<StudentOutputDTO>)responseDTO.Data;
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new StudentDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentDTO studentDTO)
        {
           

            var responseDTO = await apiService.RequestAPI<StudentDTO>("https://universityapisteven.azurewebsites.net/",
                "api/Students/",
                studentDTO,
                ApiService.Method.Post,
                false);

            if (responseDTO.Code == (int)HttpStatusCode.Created)
                return RedirectToAction(nameof(Index));

            return View(studentDTO);
        }

        [HttpGet]
        public async Task <IActionResult >Edit( int id)
        {
            //api/Students/GetStudent/{id}
            var responseDTO = await apiService.RequestAPI<StudentOutputDTO>("https://universityapisteven.azurewebsites.net/",
              "api/Students/"+ id,
              null,
              ApiService.Method.Get,
              false);


            var student = (StudentOutputDTO)responseDTO.Data;

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentOutputDTO studentDTO)
        {


            var responseDTO = await apiService.RequestAPI<StudentDTO>("https://universityapisteven.azurewebsites.net/",
                "api/Students/"+ studentDTO.ID,
                studentDTO,
                ApiService.Method.Put,
                false);

            if (responseDTO.Code == (int)HttpStatusCode.NoContent)
                return RedirectToAction(nameof(Index));

            return View(studentDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //api/Students/GetStudent/{id}
            var responseDTO = await apiService.RequestAPI<StudentOutputDTO>("https://universityapisteven.azurewebsites.net/",
              "api/Students/" + id,
              null,
              ApiService.Method.Get,
              false);


            var student = (StudentOutputDTO)responseDTO.Data;

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentOutputDTO studentDTO)
        {
            //api/Students/GetStudent/{id}
            var responseDTO = await apiService.RequestAPI<StudentOutputDTO>("https://universityapisteven.azurewebsites.net//",
              "api/Students/" + studentDTO.ID,
              null,
              ApiService.Method.Delete,
              false);

            if (responseDTO.Code == (int)HttpStatusCode.NoContent)
                return RedirectToAction(nameof(Index));

            return View(studentDTO);
        }





    }
}
