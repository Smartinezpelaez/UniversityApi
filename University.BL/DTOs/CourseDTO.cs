using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class CourseDTO
    {
        [Required]
        [JsonProperty("CourseID")]
        public int CourseID { get; set; }

        [Required]
        [JsonProperty("Title")]
        public string Title { get; set; }

        [Required]
        [JsonProperty("Credits")]
        public int Credits { get; set; }       
    }

    public class CourseOutputDTO
    {        
        [JsonProperty("CourseID")]
        public int CourseID { get; set; }
     
        [JsonProperty("Title")]
        public string Title { get; set; }
        
        [JsonProperty("Credits")]
        public int Credits { get; set; }
    }
}
