using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class AccountForumRequest
    {
        public Guid? LearnerId { get; set; }
        public Guid? TutorId { get; set; }
        public Guid? ForumId { get; set; }
        public string? Message { get; set; }
    }
}
