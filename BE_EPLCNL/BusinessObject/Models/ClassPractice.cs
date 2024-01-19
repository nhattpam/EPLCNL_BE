using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ClassPractice
    {
        public ClassPractice()
        {
            Quizzes = new HashSet<Quiz>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? ClassTypeId { get; set; }

        public virtual ClassType? ClassType { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
