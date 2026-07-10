using System;
using System.Collections.Generic;

namespace MyTestMvc.Models.Setup;

public partial class HREmailTemplate : CommonModel
{
    public string TemplateName { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public bool IsHTML { get; set; }
    public long IdHRCompany { get; set; }


}
