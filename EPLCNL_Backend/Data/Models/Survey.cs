using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Survey
    {
        public Survey()
        {
            AccountSurveys = new HashSet<AccountSurvey>();
        }

        public Guid Id { get; set; }
        public string? SurveyQuestion { get; set; }
        public string? SurveyAnswer { get; set; }

        public virtual ICollection<AccountSurvey> AccountSurveys { get; set; }
    }
}
