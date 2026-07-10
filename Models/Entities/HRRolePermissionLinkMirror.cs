namespace MyTestMvc.Models.Setup;

public partial class HRRolePermissionLinkMirror
{
    public long Id { get; set; }

    public long IdHRRole { get; set; }

    public long IdHRPermission { get; set; }

    public bool CreateOnly { get; set; }

    public bool ReadOnly { get; set; }

    public bool EditOnly { get; set; }

    public bool DeleteOnly { get; set; }
    public long IdHRCompany { get; set; }

}
