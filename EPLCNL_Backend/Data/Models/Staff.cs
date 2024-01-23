using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Centers = new HashSet<Center>();
            Tutors = new HashSet<Tutor>();
        }

        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Center> Centers { get; set; }
        public virtual ICollection<Tutor> Tutors { get; set; }
    }
}
