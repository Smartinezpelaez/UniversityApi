using System;
using System.Collections.Generic;

#nullable disable

namespace University.BL.Models
{
    public partial class CourseInstructor
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
