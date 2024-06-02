using System;
using System.Collections.Generic;

namespace Support_Manager.DataAccess;

public partial class Program
{
    public int ProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public int? StatusId { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual Status? Status { get; set; }
}
