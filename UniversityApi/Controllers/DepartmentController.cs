using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Models;

namespace University.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly UniversityContext context;
        private readonly IMapper mapper;
        public DepartmentController(UniversityContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de Department
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            // var data = context.Courses.Select(x => x.CourseId).ToList();

            var departments = context.Departments.ToList();
            var departmentDTO = departments.Select(x => mapper.Map<DepartmentOutputDTO>(x)).OrderByDescending(x => x.DepartmentID);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = departmentDTO });
        }

        /// <summary>
        /// Obtiene un Department por su id.
        /// </summary>
        /// <param name="id">Id del Department</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")] //  api/Department/GetById/1045
        public IActionResult GetById(int id)
        {
            var departments = context.Departments.Find(id);
            if (departments == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var departmentDTO = mapper.Map<DepartmentOutputDTO>(departments);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = departmentDTO });
        }

        /// <summary>
        /// Crea un objeto department.
        /// </summary>
        /// <param name="departmentDTO">Objeto del department</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(DepartmentDTO departmentDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var departments = context.Departments.Add(mapper.Map<Department>(departmentDTO)).Entity;
                context.SaveChanges();
                departmentDTO.DepartmentID = departments.DepartmentId;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = departmentDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edita un objeto del department
        /// </summary>
        /// <param name="id">Id del department</param>
        /// <param name="departmentDTO">Objeto del department</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/department/1
        public IActionResult Edit(int id, DepartmentDTO departmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var departments = context.Departments.Find(id);
                if (departments == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(departments).State = EntityState.Detached;
                context.Departments.Update(mapper.Map<Department>(departmentDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = departmentDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un objeto del department
        /// </summary>
        /// <param name="id">Id del department</param>
        /// <returns></returns>
        [HttpDelete("{id}")] //    api/department/1
        public IActionResult Delete(int id)
        {
            try
            {
                var departments = context.Departments.Find(id);
                if (departments == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                //if (context.Departments.Any(x => x.InstructorId == id))
                //    throw new Exception("Dependencies");

           
                context.Departments.Remove(departments);
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Message = "Se ha realizado el proceso con exito." });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
    }
}
