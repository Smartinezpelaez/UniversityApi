namespace University.BL.Helpers
{
    public class Endpoints
    {
        public static string URL_BASE { get; set; } = "https://university-api-smp.azurewebsites.net";


        #region Students
        public static string GET_STUDENTS { get; set; } = "api/Students/GetAll/";
        public static string GET_STUDENT { get; set; } = "api/Students/GetById/";
        public static string POST_STUDENTS { get; set; } = "api/Students/";
        public static string PUT_STUDENTS { get; set; } = "api/Students/";
        public static string DELETE_STUDENTS { get; set; } = "api/Students/";
        #endregion

        #region Courses
        public static string GET_COURSES { get; set; } = "api/Courses/GetAll/";
        public static string GET_COURSE { get; set; } = "api/Courses/GetByID/";
        public static string POST_COURSES { get; set; } = "api/Courses/";
        public static string PUT_COURSES { get; set; } = "api/Courses/";
        public static string DELETE_COURSES { get; set; } = "api/Courses/";
        #endregion


        #region Departments
        public static string GET_DEPARTMENTS { get; set; } = "api/Department/GetAll/";
        public static string GET_DEPARTMENT { get; set; } = "api/Department/GetByID/";
        public static string POST_DEPARTMENTS { get; set; } = "api/Department/";
        public static string PUT_DEPARTMENTS { get; set; } = "api/Department/";
        public static string DELETE_DEPARTMENTS { get; set; } = "api/Department/";
        #endregion

        #region OfficeAssignments
        public static string GET_OFFICEASSIGNMENTS { get; set; } = "api/OfficeAssignment/GetAll/";
        public static string GET_OFFICEASSIGNMENT { get; set; } = "api/OfficeAssignment/GetByID/";
        public static string POST_OFFICEASSIGNMENTS { get; set; } = "api/OfficeAssignment/";
        public static string PUT_OFFICEASSIGNMENTS { get; set; } = "api/OfficeAssignment/";
        public static string DELETE_OFFICEASSIGNMENTS { get; set; } = "api/OfficeAssignment/";
        #endregion  

        #region  Instructors
        public static string GET_INSTRUCTORS { get; set; } = "api/Instructor/GetAll/";
        public static string GET_INSTRUCTOR { get; set; } = "api/Instructor/GetByID/";
        public static string POST_INSTRUCTORS { get; set; } = "api/Instructor/";
        public static string PUT_INSTRUCTORS { get; set; } = "api/Instructor/";
        public static string DELETE_INSTRUCTORS { get; set; } = "api/Instructor/";
        #endregion

    }
}
