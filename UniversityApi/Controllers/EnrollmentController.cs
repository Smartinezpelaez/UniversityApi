using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using University.BL.DTOs;
using University.BL.Models;


namespace University.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UniversityContext context;
        public EnrollmentController(UniversityContext context, IMapper mapper)

        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de enrollments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var enrollments = context.Enrollments.ToList();
            var enrollmentDTO = enrollments.Select(x => mapper.Map<EnrollmentOutputDTO>(x)).OrderByDescending(x => x.EnrollmentID);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = enrollmentDTO });
        }

        /// <summary>
        /// Obtiene un enrollment ppor su id.
        /// </summary>
        /// <param name="id">Id del enrollment</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")] //  api/Enrollments/GetById/1
        public IActionResult GetById(int id)
        {
            var enrollments = context.Enrollments.Find(id);
            if (enrollments == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var enrollmentDTO = mapper.Map<EnrollmentOutputDTO>(enrollments);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = enrollmentDTO });
        }

        /// <summary>
        /// Crea un objeto enrollment.
        /// </summary>
        /// <param name="enrollmentDTO">Objeto del estudiante</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(EnrollmentDTO enrollmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var enrollments = context.Enrollments.Add(mapper.Map<Enrollment>(enrollmentDTO)).Entity;
                context.SaveChanges();
                enrollmentDTO.EnrollmentID = enrollments.EnrollmentId;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = enrollmentDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edita un objeto de enrolmment
        /// </summary>
        /// <param name="id">Id del enrollment</param>
        /// <param name="studentDTO">Objeto del enrollment</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/Enrollments/1
        public IActionResult Edit(int id, EnrollmentDTO enrollmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var enrollments = context.Enrollments.Find(id);
                if (enrollments == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(enrollments).State = EntityState.Detached;
                context.Enrollments.Update(mapper.Map<Enrollment>(enrollmentDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = enrollmentDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un objeto de enrollment
        /// </summary>
        /// <param name="id">Id del enrollment</param>
        /// <returns></returns>
        [HttpDelete("{id}")] //    api/Enrollments/1
        public IActionResult Delete(int id)
        {
            try
            {
                var enrollments = context.Enrollments.Find(id);
                if (enrollments == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                if (context.Enrollments.Any(x => x.EnrollmentId == id))
                    throw new Exception("Dependencies");
                if (context.Enrollments.Any(x => x.CourseId == id))
                    throw new Exception("Dependencies");
                if (context.Enrollments.Any(x => x.StudentId == id))
                    throw new Exception("Dependencies");

                context.Enrollments.Remove(enrollments);
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
