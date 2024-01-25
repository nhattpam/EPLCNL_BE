using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class CenterRequest
    {
        public Guid? AccountId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public Guid? StaffId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
