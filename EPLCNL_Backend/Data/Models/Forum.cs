using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Forum
    {
        public Forum()
        {
            AccountForums = new HashSet<AccountForum>();
        }

        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<AccountForum> AccountForums { get; set; }
    }
}
