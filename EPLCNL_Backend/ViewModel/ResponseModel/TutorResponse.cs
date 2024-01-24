using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class TutorResponse
    {
        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public bool? IsFreelancer { get; set; }
        public Guid? CenterId { get; set; }
        public Guid? StaffId { get; set; }
        public virtual AccountResponse? Account { get; set; }
        public virtual CenterResponse? Center { get; set; }
        public virtual StaffResponse? Staff { get; set; }
    }
}
