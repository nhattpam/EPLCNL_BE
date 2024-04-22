using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            Materials = new HashSet<Material>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? VideoUrl { get; set; }
        public string? Reading { get; set; }
        public Guid? ModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Module? Module { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
