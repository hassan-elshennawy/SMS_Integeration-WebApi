using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities.Mora
{
    public class MoraResponseDTO
    {
        public Status Status { get; set; }
        public Data Data { get; set; }

    }
    public class Status
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public List<string> Validation_Errors { get; set; }
    }

    public class Data
    {
        public string Message { get; set; }
        public int Code { get; set; }

        public string Ref_ID { get; set; }
    }
}
