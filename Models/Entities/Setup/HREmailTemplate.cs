using System;
using System.Collections.Generic;

namespace Web.API.Models.Entities.Setup;

public partial class HREmailTemplate : CommonModel
{
    public string TemplateName { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public bool IsHTML { get; set; }
    public long IdHRCompany { get; set; }


}
