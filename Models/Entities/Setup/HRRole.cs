using System;
using System.Collections.Generic;

namespace Web.API.Models.Entities.Setup;

public partial class HRRole : CommonModel
{

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }
    public long IdHRCompany { get; set; }



    public virtual ICollection<HREmployee> HREmployees { get; set; } = [];

    public virtual ICollection<HRRolePermissionLink> HRRolePermissionLinks { get; set; } = [];
}
