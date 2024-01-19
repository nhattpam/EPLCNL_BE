using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Certificate
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
