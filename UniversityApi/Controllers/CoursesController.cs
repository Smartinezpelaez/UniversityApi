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
    public class CoursesController : ControllerBase
    {

        private readonly UniversityContext context;
        private readonly IMapper mapper;
        public CoursesController(UniversityContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        /// <summary>
        /// Obtiene una lista de cursos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
           // var data = context.Courses.Select(x => x.CourseId).ToList();
            
            var courses = context.Courses.ToList();
            var coursessDTO = courses.Select(x => mapper.Map<CourseOutputDTO>(x)).OrderByDescending(x => x.CourseID);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = coursessDTO });
        }

        /// <summary>
        /// Obtiene un curso por su id.
        /// </summary>
        /// <param name="id">Id del curso</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")] //  api/Courses/GetById/1045
        public IActionResult GetById(int id)
        {
            var courses = context.Courses.Find(id);
            if (courses == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var CourseDTO = mapper.Map<CourseOutputDTO> (courses);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = CourseDTO });
        }

        /// <summary>
        /// Crea un objeto curso.
        /// </summary>
        /// <param name="courseDTO">Objeto del curso</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CourseDTO courseDTO)
        {
            try
            {
                
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var course = context.Courses.Add(mapper.Map<Course>(courseDTO)).Entity;
                context.SaveChanges();
                courseDTO.CourseID = course.CourseId;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = courseDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edita un objeto de curso
        /// </summary>
        /// <param name="id">Id del curso</param>
        /// <param name="courseDTO">Objeto del curso</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/Students/1
        public IActionResult Edit(int id, CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var course = context.Courses.Find(id);
                if (course == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(course).State = EntityState.Detached;
                context.Courses.Update(mapper.Map<Course>(courseDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = courseDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un objeto de curso
        /// </summary>
        /// <param name="id">Id del cuso</param>
        /// <returns></returns>
        [HttpDelete("{id}")] //    api/Courses/1
        public IActionResult Delete(int id)
        {
            try
            {
                var course = context.Courses.Find(id);
                if (course == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                if (context.Enrollments.Any(x => x.CourseId == id))
                    throw new Exception("Dependencies");

                //if (context.CourseInstructors.Any(x => x.CourseId == id))
                //    throw new Exception("Dependencies");

                context.Courses.Remove(course);
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
