using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class EnrollmentDTO
    {
        [JsonProperty("EnrollmentID")]
        public int EnrollmentID { get; set; }

        [Required]
        [JsonProperty("CourseID")]
        public int CourseID { get; set; }

        [Required]
        [JsonProperty("StudentID")]
        public int StudentID { get; set; }
        
        [JsonProperty("Grade")]
        public Grade? Grade { get; set; }
    }

    public class EnrollmentOutputDTO
    {
        public EnrollmentOutputDTO()
        {
            Course = new CourseOutputDTO();
            Student = new StudentOutputDTO();
        }

        [JsonProperty("EnrollmentID")]
        public int EnrollmentID { get; set; }
        
        [JsonProperty("CourseID")]
        public int CourseID { get; set; }

        [JsonProperty("StudentID")]
        public int StudentID { get; set; }

        [JsonProperty("Grade")]
        public Grade? Grade { get; set; }

        [JsonProperty("Course")]
        public CourseOutputDTO Course { get; set; }

        [JsonProperty("Student")]
        public StudentOutputDTO Student { get; set; }
    }
}
