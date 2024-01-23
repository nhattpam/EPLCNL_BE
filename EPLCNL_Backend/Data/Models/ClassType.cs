using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ClassType
    {
        public ClassType()
        {
            ClassModules = new HashSet<ClassModule>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<ClassModule> ClassModules { get; set; }
    }
}
