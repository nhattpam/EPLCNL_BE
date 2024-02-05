using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class ForumResponse
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public virtual CourseResponse? Course { get; set; }
    }
}
