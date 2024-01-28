using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            Questions = new HashSet<Question>();
            QuizAttempts = new HashSet<QuizAttempt>();
        }

        public Guid Id { get; set; }
        public Guid? ModuleId { get; set; }
        public Guid? ClassTopicId { get; set; }
        public Guid? ClassPracticeId { get; set; }
        public string? Name { get; set; }
        public double? GradeToPass { get; set; }
        public TimeSpan? Deadline { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ClassPractice? ClassPractice { get; set; }
        public virtual ClassTopic? ClassTopic { get; set; }
        public virtual Module? Module { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
    }
}
