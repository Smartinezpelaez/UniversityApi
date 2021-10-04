using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class StudentDTO
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("LastName")]
        [Required]        
        public string LastName { get; set; }

        [JsonProperty("FirstMidName")]
        [Required]
        public string FirstMidName { get; set; }

        [JsonProperty("EnrollmentDate")]
        [Required]
        public DateTime EnrollmentDate { get; set; }
    }

    public class StudentOutputDTO
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("LastName")]        
        public string LastName { get; set; }

        [JsonProperty("FirstMidName")]        
        public string FirstMidName { get; set; }

        [JsonProperty("EnrollmentDate")]        
        public DateTime EnrollmentDate { get; set; }

        [JsonProperty("FullName")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
    }
}
