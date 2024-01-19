﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Forum
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }

        public virtual Course? Course { get; set; }
    }
}
