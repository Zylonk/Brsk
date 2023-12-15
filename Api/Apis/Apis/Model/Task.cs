
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Apis.Model;

public partial class Task
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Timeframe { get; set; }

    public string Priority { get; set; } = null!;

    public bool Done { get; set; }
    
    public virtual User User { get; set; } = null!;
}
