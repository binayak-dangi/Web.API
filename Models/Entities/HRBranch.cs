using System;
using System.Collections.Generic;

namespace MyTestMvc.Models.Setup;

public partial class HRBranch : CommonModel
{
    public string BranchName { get; set; } = null!;

    public string BranchAddress { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public string? BMEmailID { get; set; }
    public long IdHRCompany { get; set; }


    public virtual ICollection<HREmployee> HREmployees { get; set; } = [];

}
