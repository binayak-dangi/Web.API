using System;
using System.Collections.Generic;

namespace MyTestMvc.Models.Setup;

public partial class EmailHistory : CommonModel
{
    public string MailCategory { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string? Attachment { get; set; }

    public string? ContentType { get; set; }

    public bool IsHTML { get; set; }

    public int Priority { get; set; }

    public int FailureAttempt { get; set; }

    public int SuccessAttempt { get; set; }

    public string ToEmail { get; set; } = null!;

    public string? CCEmail { get; set; }

    public string Status { get; set; } = null!;

    public string? Error { get; set; }

    public string? AlternateEmailBody { get; set; }

    public string? TemplateData { get; set; }
}
