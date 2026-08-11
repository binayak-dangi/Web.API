namespace Web.API.Models.Entities.Setup;

public partial class EmailConfig : CommonModel
{
    public long IdHRCompany { get; set; }

    public long? IdHRBranch { get; set; }

    public string Provider { get; set; } = null!;

    public string? TenantId { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? Host { get; set; }

    public int? Port { get; set; }

    public bool EnableSSL { get; set; } = true;

    public string FromEmail { get; set; } = null!;

    public string? FromName { get; set; }

    public string? ReplyToEmail { get; set; }

    public bool IsDefault { get; set; } = true;

}
