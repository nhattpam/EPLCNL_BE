using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            LessonMaterials = new HashSet<LessonMaterial>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? VideoUrl { get; set; }
        public string? Reading { get; set; }
        public Guid? ModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Module? Module { get; set; }
        public virtual ICollection<LessonMaterial> LessonMaterials { get; set; }
    }
}
