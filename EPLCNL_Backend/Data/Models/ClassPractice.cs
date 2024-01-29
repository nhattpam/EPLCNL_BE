using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ClassPractice
    {
        public ClassPractice()
        {
            Quizzes = new HashSet<Quiz>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
