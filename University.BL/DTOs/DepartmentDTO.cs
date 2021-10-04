using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class DepartmentDTO
    {
        [JsonProperty("DepartmentID")]
        public int DepartmentID { get; set; }

        [Required]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("Budget")]
        public double Budget { get; set; }

        [Required]
        [JsonProperty("StartDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [JsonProperty("InstructorID")]
        public int? InstructorID { get; set; }
    }

    public class DepartmentOutputDTO
    {
        public DepartmentOutputDTO()
        {
            Instructor = new InstructorOutputDTO();
        }

        [JsonProperty("DepartmentID")]
        public int DepartmentID { get; set; }
        
        [JsonProperty("Name")]
        public string Name { get; set; }
        
        [JsonProperty("Budget")]
        public decimal Budget { get; set; }
        
        [JsonProperty("StartDate")]
        public DateTime StartDate { get; set; }
        
        [JsonProperty("InstructorID")]
        public int? InstructorID { get; set; }

        [JsonProperty("Instructor")]
        public InstructorOutputDTO Instructor { get; set; }
    }
}
