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
    public class OfficeAssignmentController : ControllerBase
    {
        private readonly UniversityContext context;
        private readonly IMapper mapper;
        public OfficeAssignmentController(UniversityContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de OfficeAssignment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            // var data = context.Courses.Select(x => x.CourseId).ToList();

            var OfficeAssignments = context.OfficeAssignments.ToList();
            var OfficeAssignmentDTO = OfficeAssignments.Select(x => mapper.Map<OfficeAssignmentOutputDTO>(x)).OrderByDescending(x => x.InstructorID);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = OfficeAssignmentDTO });
        }

        /// <summary>
        /// Obtiene un OfficeAssignment por su id.
        /// </summary>
        /// <param name="id">Id del OfficeAssignment</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")] //  api/Courses/GetById/1045
        public IActionResult GetById(int id)
        {
            var OfficeAssignments = context.OfficeAssignments.Find(id);
            if (OfficeAssignments == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var OfficeAssignmentDTO = mapper.Map<OfficeAssignmentDTO>(OfficeAssignments);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = OfficeAssignmentDTO });
        }

        /// <summary>
        /// Crea un objeto curso.
        /// </summary>
        /// <param name="OfficeAssignmentDTO">Objeto del instructor</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(OfficeAssignmentDTO OfficeAssignmentDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var OfficeAssignments = context.OfficeAssignments.Add(mapper.Map<OfficeAssignment>(OfficeAssignmentDTO)).Entity;
                context.SaveChanges();
                OfficeAssignmentDTO.InstructorID = OfficeAssignments.InstructorId;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = OfficeAssignmentDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edita un objeto del OfficeAssignment
        /// </summary>
        /// <param name="id">Id del OfficeAssignment</param>
        /// <param name="OfficeAssignmentDTO">Objeto del OfficeAssignment</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/OfficeAssignment/1
        public IActionResult Edit(int id, OfficeAssignmentDTO OfficeAssignmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var OfficeAssignments = context.OfficeAssignments.Find(id);
                if (OfficeAssignments == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(OfficeAssignments).State = EntityState.Detached;
                context.OfficeAssignments.Update(mapper.Map<OfficeAssignment>(OfficeAssignmentDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = OfficeAssignmentDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un objeto del OfficeAssignment
        /// </summary>
        /// <param name="id">Id del OfficeAssignment</param>
        /// <returns></returns>
        [HttpDelete("{id}")] //    api/OfficeAssignment/1
        public IActionResult Delete(int id)
        {
            try
            {
                var OfficeAssignments = context.OfficeAssignments.Find(id);
                if (OfficeAssignments == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                //if (context.OfficeAssignments.Any(x => x.id == id))
                //    throw new Exception("Dependencies");

                context.OfficeAssignments.Remove(OfficeAssignments);
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
