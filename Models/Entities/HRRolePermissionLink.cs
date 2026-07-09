namespace MyTestMvc.Models.Setup;

public partial class HRRolePermissionLink : CommonModel
{
    public long IdHRRole { get; set; }

    public long IdHRPermission { get; set; }

    public bool CreateOnly { get; set; }

    public bool ReadOnly { get; set; }

    public bool EditOnly { get; set; }

    public bool DeleteOnly { get; set; }

    public virtual HRPermission HRPermission { get; set; } = null!;

    public virtual HRRole HRRole { get; set; } = null!;
    public virtual ICollection<HREmployeePermissionLink> HREmployeePermissionLinks { get; set; } = [];
}
