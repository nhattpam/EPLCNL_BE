using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class CourseType
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
    }
}
