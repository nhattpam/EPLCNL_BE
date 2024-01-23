using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Center
    {
        public Center()
        {
            Tutors = new HashSet<Tutor>();
        }

        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public Guid? StaffId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Staff? Staff { get; set; }
        public virtual ICollection<Tutor> Tutors { get; set; }
    }
}
