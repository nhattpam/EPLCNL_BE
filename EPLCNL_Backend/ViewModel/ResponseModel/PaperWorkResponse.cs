using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class PaperWorkResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PaperWorkUrl { get; set; }
        public Guid? PaperWorkTypeId { get; set; }
        public Guid? TutorId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual PaperWorkTypeResponse? PaperWorkType { get; set; }
    }
}
