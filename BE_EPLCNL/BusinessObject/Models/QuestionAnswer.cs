using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class QuestionAnswer
    {
        public Guid Id { get; set; }
        public Guid? QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public int? Position { get; set; }
        public bool? IsAnswer { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Question? Question { get; set; }
    }
}
