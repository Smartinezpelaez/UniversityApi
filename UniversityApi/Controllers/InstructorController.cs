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
    public class InstructorController : ControllerBase
    {
        private readonly UniversityContext context;
        private readonly IMapper mapper;
        public InstructorController(UniversityContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de Instructores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            // var data = context.Courses.Select(x => x.CourseId).ToList();

            var instructors = context.Instructors.ToList();
            var instructorDTO = instructors.Select(x => mapper.Map<InstructorOutputDTO>(x)).OrderByDescending(x => x.ID);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = instructorDTO });
        }

        /// <summary>
        /// Obtiene un instructor por su id.
        /// </summary>
        /// <param name="id">Id del instructor</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")] //  api/Courses/GetById/1045
        public IActionResult GetById(int id)
        {
            var instructors = context.Instructors.Find(id);
            if (instructors == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var instructorDTO = mapper.Map<InstructorOutputDTO>(instructors);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = instructorDTO });
        }

        /// <summary>
        /// Crea un objeto curso.
        /// </summary>
        /// <param name="instructorDTO">Objeto del instructor</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(InstructorDTO instructorDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var instructors = context.Instructors.Add(mapper.Map<Instructor>(instructorDTO)).Entity;
                context.SaveChanges();
                instructorDTO.ID = instructors.Id;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = instructorDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edita un objeto del instructor
        /// </summary>
        /// <param name="id">Id del instructor</param>
        /// <param name="instructorDTO">Objeto del instructor</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/Instructor/1
        public IActionResult Edit(int id, InstructorDTO instructorDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var instructors = context.Instructors.Find(id);
                if (instructors == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(instructors).State = EntityState.Detached;
                context.Instructors.Update(mapper.Map<Instructor>(instructorDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = instructorDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un objeto del instructor
        /// </summary>
        /// <param name="id">Id del instructor</param>
        /// <returns></returns>
        [HttpDelete("{id}")] //    api/Instructor/1
        public IActionResult Delete(int id)
        {
            try
            {
                var instructors = context.Instructors.Find(id);
                if (instructors == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                if (context.Departments.Any(x => x.InstructorId == id))
                    throw new Exception("Dependencies");

                if (context.OfficeAssignments.Any(x => x.InstructorId == id))
                    throw new Exception("Dependencies");

                if (context.CourseInstructors.Any(x => x.InstructorId == id))
                    throw new Exception("Dependencies");
                context.Instructors.Remove(instructors);
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
