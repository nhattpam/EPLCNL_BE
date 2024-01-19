﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class LessonMaterial
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? MaterialUrl { get; set; }
        public Guid? LessonId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Lesson? Lesson { get; set; }
    }
}
