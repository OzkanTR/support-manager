using System;
using System.Collections.Generic;

namespace Support_Manager.DataAccess;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? AddressDetail { get; set; }

    public int? StatusId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? FollowerId { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual Follower? Follower { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual Status? Status { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
