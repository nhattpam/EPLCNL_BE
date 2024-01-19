using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Feedback
    {
        public Guid Id { get; set; }
        public string? FeedbackContent { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? CourseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Course? Course { get; set; }
    }
}
