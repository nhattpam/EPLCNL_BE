using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Data.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]public virtual ICollection<Account> Accounts { get; set; }
    }
}
