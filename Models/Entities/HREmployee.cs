using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Web.API.Models.Entities;

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

    public long? PasswordExpiryPeriod { get; set; }

    public string? PasswordHistory { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public long? LoginAttemptCount { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? PasswordResetTokenExpiry { get; set; }
    public long IdHRCompany { get; set; }


    [ForeignKey(nameof(IdHRRole))]
    public virtual HRRole HRRole { get; set; } = null!;

    [ForeignKey(nameof(IdHRBranch))]
    public virtual HRBranch HRBranch { get; set; } = null!;

    [ForeignKey(nameof(IdHRCorporateTitle))]
    public virtual HRCorporateTitle HRCorporateTitle { get; set; } = null!;

    [ForeignKey(nameof(IdHRFunctionalTitle))]
    public virtual HRFunctionalTitle HRFunctionalTitle { get; set; } = null!;
    // Navigation Property
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
