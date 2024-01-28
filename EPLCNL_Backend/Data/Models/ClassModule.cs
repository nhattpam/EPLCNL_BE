﻿using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ClassModule
    {
        public Guid Id { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ClassLesson? ClassLesson { get; set; }
    }
}
