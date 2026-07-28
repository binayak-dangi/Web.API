using System;
using System.Collections.Generic;

namespace Web.API.Models.Entities.Setup;

public partial class HRCompany : CommonModel
{
    public string CompanyName { get; set; } = null!;

    public int LoginAttempt { get; set; }
}
