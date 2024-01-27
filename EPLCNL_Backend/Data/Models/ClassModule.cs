using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ClassModule
    {
        public ClassModule()
        {
            ClassLessons = new HashSet<ClassLesson>();
        }

        public Guid Id { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<ClassLesson> ClassLessons { get; set; }
    }
}
