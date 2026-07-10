using System;
using System.Collections.Generic;

namespace MyTestMvc.Models.Setup;

public partial class HRPermission : CommonModel
{
    public long IdParentPermission { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Area { get; set; } = null!;

    public string Controller { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string PermissionCode { get; set; } = null!;

    public long ParentOrder { get; set; }

    public long? ChildOrder { get; set; }

    public string? FontIcon { get; set; }
    public string? AreaIcon { get; set; }
    public long IdHRCompany { get; set; }



    public virtual ICollection<HRRolePermissionLink> HRRolePermissionLinks { get; set; } = [];
}
