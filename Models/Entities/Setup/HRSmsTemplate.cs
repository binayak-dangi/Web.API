using System;

namespace Web.API.Models.Entities.Setup;

public partial class HRSmsTemplate : CommonModel
{
    public long IdHRCompany { get; set; }

    public string TemplateName { get; set; } = null!;

    public string TemplateCode { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Description { get; set; }

    public long Priority { get; set; }
}
