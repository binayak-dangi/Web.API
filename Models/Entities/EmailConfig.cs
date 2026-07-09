using System;
using System.Collections.Generic;

namespace MyTestMvc.Models.Setup;

public partial class EmailConfig : CommonModel
{
    public long IdEmailTemplate { get; set; }

    public string ToMail { get; set; } = null!;

    public string? CCMail { get; set; }

    public bool BranchFilter { get; set; }

    public virtual HREmailTemplate HREmailTemplate { get; set; } = null!;
}
