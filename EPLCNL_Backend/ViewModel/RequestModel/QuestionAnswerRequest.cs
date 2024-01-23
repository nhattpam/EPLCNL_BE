using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class QuestionAnswerRequest
    {
        public Guid? QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public int? Position { get; set; }
        public bool? IsAnswer { get; set; }
    }
}
