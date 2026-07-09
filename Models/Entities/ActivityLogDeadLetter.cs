using System;
using System.Collections.Generic;

namespace MyTestMvc.Models.Setup;

public partial class ActivityLogDeadLetter
{
        public long Id { get; set; }
        public string Payload { get; set; } = null!;
        public string Error { get; set; } = null!;
        public DateTime FailedOn { get; set; } = DateTime.Now;
}
