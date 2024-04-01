using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class CourseResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? TutorId { get; set; }
        public decimal? StockPrice { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOnlineClass { get; set; }
        public double? Rating { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Tags { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual CategoryResponse? Category { get; set; }
		public virtual TutorResponse? Tutor { get; set; }
		public virtual CertificateResponse? Certificate { get; set; }
        public string? Note { get; set; }


    }
}
