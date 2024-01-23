using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class PaperWorkRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PaperWorkUrl { get; set; }
        public Guid? PaperWorkTypeId { get; set; }
        public Guid? TutorId { get; set; }
    }
}
