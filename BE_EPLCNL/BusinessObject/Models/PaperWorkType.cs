using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class PaperWorkType
    {
        public PaperWorkType()
        {
            PaperWorks = new HashSet<PaperWork>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<PaperWork> PaperWorks { get; set; }
    }
}
