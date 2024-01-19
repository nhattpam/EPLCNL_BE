using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class CoursePlatform
    {
        public CoursePlatform()
        {
            Courses = new HashSet<Course>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CourseTypeId { get; set; }

        public virtual CourseType? CourseType { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
