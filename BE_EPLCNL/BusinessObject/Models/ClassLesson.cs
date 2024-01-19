using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ClassLesson
    {
        public ClassLesson()
        {
            ClassTopics = new HashSet<ClassTopic>();
        }

        public Guid Id { get; set; }
        public string? ClassHours { get; set; }
        public string? ClassUrl { get; set; }
        public Guid? ClassModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ClassModule? ClassModule { get; set; }
        public virtual ICollection<ClassTopic> ClassTopics { get; set; }
    }
}
