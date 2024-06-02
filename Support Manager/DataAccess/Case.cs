using System;
using System.Collections.Generic;

namespace Support_Manager.DataAccess;

public partial class Case
{
    public int CaseId { get; set; }

    public string? CaseNumber { get; set; }

    public string CaseName { get; set; } = null!;

    public string CaseMessage { get; set; } = null!;

    public int? FollowerId { get; set; }

    public int? CustomerId { get; set; }

    public int? UserId { get; set; }

    public int? StatusId { get; set; }

    public int? CreatedUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public int? UpdateUserId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? ProgramId { get; set; }

    public virtual User? CreatedUser { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Follower? Follower { get; set; }

    public virtual Program? Program { get; set; }

    public virtual Status? Status { get; set; }

    public virtual User? UpdateUser { get; set; }

    public virtual User? User { get; set; }
}
