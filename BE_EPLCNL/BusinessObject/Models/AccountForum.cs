using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class AccountForum
    {
        public Guid? AccountId { get; set; }
        public Guid? ForumId { get; set; }
        public string? Message { get; set; }
        public DateTime? MessagedDate { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Forum? Forum { get; set; }
    }
}
