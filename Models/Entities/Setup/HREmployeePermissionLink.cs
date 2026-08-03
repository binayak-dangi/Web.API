using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.Models.Entities.Setup;

public class HREmployeePermissionLink
{
    public long Id { get; set; }

    public long IdHREmployee { get; set; }
    public long IdHRCompany { get; set; }


    public long IdHRPermission { get; set; }

    public bool CreateOnly { get; set; }

    public bool ReadOnly { get; set; }

    public bool EditOnly { get; set; }

    public bool DeleteOnly { get; set; }


    // Navigation Properties
    [ForeignKey(nameof(IdHREmployee))]
    public virtual HREmployee HREmployee { get; set; } = null!;
    [ForeignKey(nameof(IdHRPermission))]
    public virtual HRPermission HRPermission { get; set; } = null!;
}
