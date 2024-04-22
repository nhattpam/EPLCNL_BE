using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class QuestionRequest
    {
        public string? QuestionText { get; set; }
        public string? QuestionImageUrl { get; set; }
        public string? QuestionAudioUrl { get; set; }
        public double? DefaultGrade { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? QuizId { get; set; }
        public bool? IsActive { get; set; }
    }
}
