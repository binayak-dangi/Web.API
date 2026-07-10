using System;
using System.Collections.Generic;

namespace MyTestMvc.Models.Setup;

public partial class HRRole : CommonModel
{

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }
    public long IdHRCompany { get; set; }



    public virtual ICollection<HREmployee> HREmployees { get; set; } = [];

    public virtual ICollection<HRRolePermissionLink> HRRolePermissionLinks { get; set; } = [];
}
