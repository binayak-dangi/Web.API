namespace MyTestMvc.Models.Setup;

public class HREmployeePermissionLink
{
    public long Id { get; set; }

    public long IDHREmployee { get; set; }
    public long IdHRCompany { get; set; }


    public long IDHRPermission { get; set; }

    public bool CreateOnly { get; set; }

    public bool ReadOnly { get; set; }

    public bool EditOnly { get; set; }

    public bool DeleteOnly { get; set; }

    public string ClientIP { get; set; }

    // Navigation Properties
    public virtual HREmployee HREmployee { get; set; }

    public virtual HRPermission HRPermission { get; set; }
}
