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


        #region Departments
        public static string GET_DEPARTMENTS { get; set; } = "api/department/GetAll";
        public static string GET_DEPARTMENT { get; set; } = "api/department/GetByID";
        public static string POST_DEPARTMENT { get; set; } = "api/department/";
        public static string PUT_DEPARTMENT { get; set; } = "api/department/";
        public static string DELETE_DEPARTMENT { get; set; } = "api/department/";

        #endregion




        public static string GET_INSTRUCTORS { get; set; } = "api/Instructor/GetAll";

    }
}
