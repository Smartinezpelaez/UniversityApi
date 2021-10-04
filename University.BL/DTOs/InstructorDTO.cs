using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class InstructorDTO
    {        
        [JsonProperty("ID")]
        public int ID { get; set; }

        [Required]
        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [Required]
        [JsonProperty("FirstMidName")]
        public string FirstMidName { get; set; }

        [Required]
        [JsonProperty("HireDate")]
        public DateTime HireDate { get; set; }
    }

    public class InstructorOutputDTO
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
        
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        
        [JsonProperty("FirstMidName")]
        public string FirstMidName { get; set; }
        
        [JsonProperty("HireDate")]
        public DateTime HireDate { get; set; }

        [JsonProperty("Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
    }
}
