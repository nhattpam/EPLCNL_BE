using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Module
    {
        public Module()
        {
            Assignments = new HashSet<Assignment>();
            Lessons = new HashSet<Lesson>();
            Quizzes = new HashSet<Quiz>();
        }

        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
