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
    public class CourseInstructorController : ControllerBase
    {
        private readonly UniversityContext context;
        private readonly IMapper mapper;
        public CourseInstructorController (UniversityContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de los instructores del curso
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            // var data = context.Courses.Select(x => x.CourseId).ToList();

            var coursesInstructor = context.CourseInstructors.ToList();
            var coursessDTO = coursesInstructor.Select(x => mapper.Map<CourseInstructorOutputDTO>(x)).OrderByDescending(x => x.CourseID);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = coursessDTO });
        }

        /// <summary>
        /// Obtiene el instructor del curso por su id.
        /// </summary>
        /// <param name="id">Id del curso</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")] //  api/Courses/GetById/1045
        public IActionResult GetById(int id)
        {
            var coursesInstructor = context.CourseInstructors.Find(id);
            if (coursesInstructor == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var CourseDTO = mapper.Map<CourseInstructorOutputDTO>(coursesInstructor);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = CourseDTO });
        }

        /// <summary>
        /// Crea un objeto instructor del curso.
        /// </summary>
        /// <param name="coursesInstructorDTO">Objeto instrcutor del curso</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CourseInstructorDTO coursesInstructorDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var coursesInstructor = context.CourseInstructors.Add(mapper.Map<CourseInstructor>(coursesInstructorDTO)).Entity;
                context.SaveChanges();
                coursesInstructorDTO.ID = coursesInstructor.Id;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = coursesInstructorDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edita un instructor del curso
        /// </summary>
        /// <param name="id">Id del instructor del curso</param>
        /// <param name="courseDTO">Objeto del instructor del curso</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/Students/1
        public IActionResult Edit(int id, CourseInstructorDTO coursesInstructorDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var coursesInstructor = context.CourseInstructors.Find(id);
                if (coursesInstructor == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(coursesInstructor).State = EntityState.Detached;
                context.CourseInstructors.Update(mapper.Map<CourseInstructor>(coursesInstructorDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = coursesInstructorDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un instrutor del de curso
        /// </summary>
        /// <param name="id">Id del instructor del cuso</param>
        /// <returns></returns>
        [HttpDelete("{id}")] //    api/Courses/1
        public IActionResult Delete(int id)
        {
            try
            {
                var coursesInstructor = context.CourseInstructors.Find(id);
                if (coursesInstructor == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                if (context.Courses.Any(x => x.CourseId == id))
                    throw new Exception("Dependencies");

                if (context.Instructors.Any(x => x.Id == id))
                    throw new Exception("Dependencies");

                context.CourseInstructors.Remove(coursesInstructor);
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
