using System;
using System.Collections.Generic;

namespace MyTestMvc.Models.Setup;

public partial class HRCompany : CommonModel
{
    public string CompanyName { get; set; } = null!;

    public int LoginAttempt { get; set; }
}
