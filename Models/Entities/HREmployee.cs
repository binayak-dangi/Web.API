using System;
using System.Collections.Generic;
using Web.API.Entities;

namespace MyTestMvc.Models.Setup;

public partial class HREmployee : CommonModel
{
    public long IdHRRole { get; set; }

    public long IdHRBranch { get; set; }
    public long IdHRCorporateTitle { get; set; }
    public long IdHRFunctionalTitle { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string Designation { get; set; } = null!;

    public DateOnly? DOB { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PermanentAddress { get; set; }

    public string? TemporaryAddress { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime? LastPasswordAlterDate { get; set; }

    public string? ImageGUID { get; set; }

    public int? PasswordExpiryPeriod { get; set; }

    public string? PasswordHistory { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public int? LoginAttemptCount { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? PasswordResetTokenExpiry { get; set; }

    public virtual HRRole HRRole { get; set; } = null!;

    public virtual HRBranch HRBranch { get; set; } = null!;

    public virtual ICollection<HREmployeePermissionLink> HREmployeePermissionLink { get; set; } = [];
    public virtual HRCorporateTitle HRCorporateTitle { get; set; } = null!;
    public virtual HRFunctionalTitle HRFunctionalTitle { get; set; } = null!;
}
