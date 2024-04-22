using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public string? QuestionText { get; set; }
        public string? QuestionImageUrl { get; set; }
        public string? QuestionAudioUrl { get; set; }
        public double? DefaultGrade { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? QuizId { get; set; }
        public bool? IsActive { get; set; }
        public virtual QuizResponse? Quiz { get; set; }

    }
}
