using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.Models.Entities.Setup;

public partial class HRRolePermissionLink
{
    public long Id { get; set; }
    public long IdHRRole { get; set; }

    public long IdHRPermission { get; set; }

    public bool CreateOnly { get; set; }

    public bool ReadOnly { get; set; }

    public bool EditOnly { get; set; }

    public bool DeleteOnly { get; set; }
    public long IdHRCompany { get; set; }

    [ForeignKey(nameof(IdHRPermission))]
    public virtual HRPermission HRPermission { get; set; } = null!;

    [ForeignKey(nameof(IdHRRole))]
    public virtual HRRole HRRole { get; set; } = null!;
}
