using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using University.BL.DTOs;
using University.BL.Models;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UniversityContext context;
        public StudentsController(UniversityContext context, IMapper mapper)

        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de estudiantes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var students = context.Students.ToList();
            var studentsDTO = students.Select(x => mapper.Map<StudentOutputDTO>(x)).OrderByDescending(x => x.ID);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = studentsDTO });
        }

        /// <summary>
        /// Obtiene un estudiante ppor su id.
        /// </summary>
        /// <param name="id">Id del estudiante</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")] //  api/Students/GetById/1
        public IActionResult GetById(int id)
        {
            var student = context.Students.Find(id);
            if (student == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var studentDTO = mapper.Map<StudentOutputDTO>(student);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = studentDTO });
        }

        /// <summary>
        /// Crea un objeto estudiante.
        /// </summary>
        /// <param name="studentDTO">Objeto del estudiante</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(StudentDTO studentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var student = context.Students.Add(mapper.Map<Student>(studentDTO)).Entity;
                context.SaveChanges();
                studentDTO.ID = student.Id;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = studentDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edita un objeto de estudiante
        /// </summary>
        /// <param name="id">Id del estudiante</param>
        /// <param name="studentDTO">Objeto del estudiante</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/Students/1
        public IActionResult Edit(int id, StudentDTO studentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var student = context.Students.Find(id);
                if (student == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(student).State = EntityState.Detached;
                context.Students.Update(mapper.Map<Student>(studentDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = studentDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un objeto de estudiante
        /// </summary>
        /// <param name="id">Id del estudiante</param>
        /// <returns></returns>
        [HttpDelete("{id}")] //    api/Students/1
        public IActionResult Delete(int id)
        {
            try
            {
                var student = context.Students.Find(id);
                if (student == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                if (context.Enrollments.Any(x => x.StudentId == id))
                    throw new Exception("Dependencies");

                context.Students.Remove(student);
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