using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class AccountSurvey
    {
        public Guid Id { get; set; }
        public Guid? SurveyId { get; set; }
        public Guid? LearnerId { get; set; }
        public string? Answer { get; set; }

        public virtual Learner? Learner { get; set; }
        public virtual Survey? Survey { get; set; }
    }
}
