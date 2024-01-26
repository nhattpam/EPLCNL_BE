using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class CloudFoneRequest
    {
        [Key]
        public string? ApiKey { get; set; }

        public string? CallNumber { get; set; }

        public string? CallName { get; set; }

        public string? QueueNumber { get; set; }

        public string? ReceiptNumber { get; set; }

        public string? Key { get; set; }

        public string? KeyRinging { get; set; }

        public string? Status { get; set; }

        public string? Direction { get; set; }

        public string? NumberPbx { get; set; }

        public string? Message { get; set; }

        public DataCloudPhoneEntity Data { get; set; }
    }
}
