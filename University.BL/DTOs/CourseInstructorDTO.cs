using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class CourseInstructorDTO
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [Required]
        [JsonProperty("CourseID")]
        public int CourseID { get; set; }

        [Required]
        [JsonProperty("InstructorID")]
        public int InstructorID { get; set; }
    }

    public class CourseInstructorOutputDTO
    {
        public CourseInstructorOutputDTO()
        {
            Course = new CourseOutputDTO();
            Instructor = new InstructorOutputDTO();
        }

        [JsonProperty("ID")]
        public int ID { get; set; }
        
        [JsonProperty("CourseID")]
        public int CourseID { get; set; }
        
        [JsonProperty("InstructorID")]
        public int InstructorID { get; set; }
        
        [JsonProperty("Course")]
        public CourseOutputDTO Course { get; set; }

        [JsonProperty("Instructor")]
        public InstructorOutputDTO Instructor { get; set; }
    }
}
