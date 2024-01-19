using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ClassModule
    {
        public ClassModule()
        {
            ClassLessons = new HashSet<ClassLesson>();
        }

        public Guid Id { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? ClassTypeId { get; set; }

        public virtual ClassType? ClassType { get; set; }
        public virtual ICollection<ClassLesson> ClassLessons { get; set; }
    }
}
