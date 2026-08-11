namespace Web.API.Models.Entities.Setup;

public partial class SmsConfig : CommonModel
{
    public long IdHRCompany { get; set; }

    public long? IdHRBranch { get; set; }

    public string Provider { get; set; } = null!;

    public string? AccountId { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? ApiKey { get; set; }

    public string SenderId { get; set; } = null!;

    public string? BaseUrl { get; set; }

    public bool IsDefault { get; set; } = true;

}
