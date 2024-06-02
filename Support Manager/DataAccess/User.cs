using System;
using System.Collections.Generic;

namespace Support_Manager.DataAccess;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? CustomerId { get; set; }

    public string UserTitle { get; set; } = null!;

    public int? StatusId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Case> CaseCreatedUsers { get; set; } = new List<Case>();

    public virtual ICollection<Case> CaseUpdateUsers { get; set; } = new List<Case>();

    public virtual ICollection<Case> CaseUsers { get; set; } = new List<Case>();

    public virtual Customer? Customer { get; set; }

    public virtual Status? Status { get; set; }
}
