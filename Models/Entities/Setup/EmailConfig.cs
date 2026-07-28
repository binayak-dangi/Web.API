using System;
using System.Collections.Generic;

namespace Web.API.Models.Entities.Setup;

public partial class EmailConfig : CommonModel
{
    public long IdEmailTemplate { get; set; }

    public string ToMail { get; set; } = null!;

    public string? CCMail { get; set; }

    public bool BranchFilter { get; set; }
    public long IdHRCompany { get; set; }


    public virtual HREmailTemplate HREmailTemplate { get; set; } = null!;
}
