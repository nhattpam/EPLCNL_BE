using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class SurveyResponse
    {
        public Guid Id { get; set; }
        public string? SurveyQuestion { get; set; }
        public string? SurveyAnswer { get; set; }
    }
}
