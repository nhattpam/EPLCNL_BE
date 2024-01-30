using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class QuestionAnswerResponse
    {
        public Guid Id { get; set; }
        public Guid? QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public int? Position { get; set; }
        public bool? IsAnswer { get; set; }
        public virtual QuestionResponse? Question { get; set; }

    }
}
