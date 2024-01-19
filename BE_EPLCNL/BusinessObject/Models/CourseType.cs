using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class CourseType
    {
        public CourseType()
        {
            CoursePlatforms = new HashSet<CoursePlatform>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<CoursePlatform> CoursePlatforms { get; set; }
    }
}
