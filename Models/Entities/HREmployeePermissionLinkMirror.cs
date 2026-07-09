namespace MyTestMvc.Models.Setup;

public class HREmployeePermissionLinkMirror
{
    public long Id { get; set; }

    public long IDHREmployee { get; set; }

    public long IDHRPermission { get; set; }

    public bool CreateOnly { get; set; }

    public bool ReadOnly { get; set; }

    public bool EditOnly { get; set; }

    public bool DeleteOnly { get; set; }
}