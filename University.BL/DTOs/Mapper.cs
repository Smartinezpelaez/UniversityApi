using AutoMapper;
using University.BL.Models;

namespace University.BL.DTOs
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Student, StudentOutputDTO>();
            CreateMap<StudentDTO, Student>();

            CreateMap<Enrollment, EnrollmentOutputDTO>();
            CreateMap<EnrollmentDTO, Enrollment>();            

            CreateMap<Course, CourseOutputDTO>();
            CreateMap<CourseDTO, Course>();            

            CreateMap<Instructor, InstructorOutputDTO>();
            CreateMap<InstructorDTO, Instructor>();

            CreateMap<Department, DepartmentOutputDTO>();
            CreateMap<DepartmentDTO, Department>();

            CreateMap<CourseInstructor, CourseInstructorOutputDTO>();
            CreateMap<CourseInstructorDTO, CourseInstructor>();
            
            CreateMap<OfficeAssignment, OfficeAssignmentOutputDTO>();
            CreateMap<OfficeAssignmentDTO, OfficeAssignment>();
        }
    }
}
