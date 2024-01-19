using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ClassTopic
    {
        public ClassTopic()
        {
            ClassMaterials = new HashSet<ClassMaterial>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MaterialUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? ClassLessonId { get; set; }

        public virtual ClassLesson? ClassLesson { get; set; }
        public virtual ICollection<ClassMaterial> ClassMaterials { get; set; }
    }
}
