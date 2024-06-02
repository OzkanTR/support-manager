using System;
using System.Collections.Generic;

namespace Support_Manager.DataAccess;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? ServiceNumber { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ServiceMessage { get; set; } = null!;

    public int? FollowerId { get; set; }

    public int? CustomerId { get; set; }

    public int? StatusId { get; set; }

    public int? CreatedUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public int? UpdateUserId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Follower? CreatedUser { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Follower? Follower { get; set; }

    public virtual Status? Status { get; set; }

    public virtual Follower? UpdateUser { get; set; }
}
