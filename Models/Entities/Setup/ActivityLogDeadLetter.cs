using System;
using System.Collections.Generic;

namespace Web.API.Models.Entities.Setup;

public partial class ActivityLogDeadLetter
{
    public long Id { get; set; }
    public string Payload { get; set; } = null!;
    public string Error { get; set; } = null!;
    public DateTime FailedOn { get; set; } = DateTime.Now;
    public long IdHRCompany { get; set; }

}
