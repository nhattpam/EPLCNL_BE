using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Category
    {
        public Category()
        {
            CourseTypes = new HashSet<CourseType>();
            Courses = new HashSet<Course>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<CourseType> CourseTypes { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
