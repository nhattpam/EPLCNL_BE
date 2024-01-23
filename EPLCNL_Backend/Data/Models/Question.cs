using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Question
    {
        public Question()
        {
            QuestionAnswers = new HashSet<QuestionAnswer>();
        }

        public Guid Id { get; set; }
        public string? QuestionText { get; set; }
        public string? QuestionImageUrl { get; set; }
        public string? QuestionAudioUrl { get; set; }
        public double? DefaultGrade { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? QuizId { get; set; }

        public virtual Quiz? Quiz { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
