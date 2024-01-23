using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class AccountSurveyRequest
    {
        public Guid? SurveyId { get; set; }
        public Guid? LearnerId { get; set; }
        public string? Answer { get; set; }
    }
}
