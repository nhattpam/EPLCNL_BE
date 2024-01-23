using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Learner
    {
        public Learner()
        {
            AccountSurveys = new HashSet<AccountSurvey>();
            AssignmentAttempts = new HashSet<AssignmentAttempt>();
            Enrollments = new HashSet<Enrollment>();
            QuizAttempts = new HashSet<QuizAttempt>();
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<AccountSurvey> AccountSurveys { get; set; }
        public virtual ICollection<AssignmentAttempt> AssignmentAttempts { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
