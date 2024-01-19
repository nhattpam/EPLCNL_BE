using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Center
    {
        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        public virtual Account? Account { get; set; }
    }
}
