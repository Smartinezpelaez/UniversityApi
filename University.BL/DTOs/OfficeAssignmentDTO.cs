using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class OfficeAssignmentDTO
    {
        [Required]
        [JsonProperty("InstructorID")]
        public int InstructorID { get; set; }

        [Required]
        [JsonProperty("Location")]
        public string Location { get; set; }                        
    }

    public class OfficeAssignmentOutputDTO
    {
        public OfficeAssignmentOutputDTO()
        {
            Instructor = new InstructorOutputDTO();
        }
        
        [JsonProperty("InstructorID")]
        public int InstructorID { get; set; }
        
        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("Instructor")]
        public InstructorOutputDTO Instructor { get; set; }
    }
}
