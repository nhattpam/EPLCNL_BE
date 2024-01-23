using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class AccountForumResponse
    {
        public Guid Id { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? TutorId { get; set; }
        public Guid? ForumId { get; set; }
        public string? Message { get; set; }
        public DateTime? MessagedDate { get; set; }
    }
}
