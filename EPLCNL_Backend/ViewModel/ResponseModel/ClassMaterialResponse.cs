﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class ClassMaterialResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? MaterialUrl { get; set; }
        public Guid? ClassTopicId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
