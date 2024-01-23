using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Tutor
    {
        public Tutor()
        {
            Courses = new HashSet<Course>();
            PaperWorks = new HashSet<PaperWork>();
        }

        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public bool? IsFreelancer { get; set; }
        public Guid? CenterId { get; set; }
        public Guid? StaffId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Center? Center { get; set; }
        public virtual Staff? Staff { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<PaperWork> PaperWorks { get; set; }
    }
}
