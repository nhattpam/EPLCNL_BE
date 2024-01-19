using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class AccountSurvey
    {
        public Guid Id { get; set; }
        public Guid? SurveyId { get; set; }
        public Guid? AccountId { get; set; }
        public string? Answer { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Survey? Survey { get; set; }
    }
}
