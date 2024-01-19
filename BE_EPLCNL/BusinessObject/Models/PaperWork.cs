using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class PaperWork
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PaperWorkUrl { get; set; }
        public Guid? PaperWorkTypeId { get; set; }
        public Guid? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual PaperWorkType? PaperWorkType { get; set; }
    }
}
