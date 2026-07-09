using System;
using System.Collections.Generic;

namespace MyTestMvc.Models.Setup;

public partial class ActivityLog 
{
    public long Id { get; set; }
    public long IdEmployee { get; set; }

    public string Username { get; set; } = null!;

    public string Action { get; set; } = null!;
    
    public string? Controller { get; set; }
    
    public string? Method { get; set; }
    
    public string? Parameters { get; set; }
    
    public string? IPAddress { get; set; }
    
    public string? UserAgent { get; set; }
    
    public DateTime Created_On { get; set; } = DateTime.Now;
    public Guid IdGUID { get; set; } = Guid.NewGuid();

}
