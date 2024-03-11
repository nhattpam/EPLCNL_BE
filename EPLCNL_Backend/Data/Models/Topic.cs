using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Materials = new HashSet<Material>();
            Quizzes = new HashSet<Quiz>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? ClassLessonId { get; set; }

        public virtual ClassLesson? ClassLesson { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
