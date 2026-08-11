namespace Web.API.Models.Entities.Setup;

public partial class EventLog : CommonModel
{
    public long IdHRCompany { get; set; }

    public string EventType { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string? Attachment { get; set; }

    public string? ContentType { get; set; }

    public bool IsHTML { get; set; }

    public long Priority { get; set; }

    public long FailureAttempt { get; set; }

    public long SuccessAttempt { get; set; }

    public string ToEmail { get; set; } = null!;

    public string? CCEmail { get; set; }

    public string Status { get; set; } = null!;

    public string? Error { get; set; }

    public string? AlternateEmailBody { get; set; }

    public string? TemplateData { get; set; }
}
