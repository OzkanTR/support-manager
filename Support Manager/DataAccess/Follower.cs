using System;
using System.Collections.Generic;

namespace Support_Manager.DataAccess;

public partial class Follower
{
    public int FollowerUserId { get; set; }

    public string FollowerUserName { get; set; } = null!;

    public string FollowerPassword { get; set; } = null!;

    public int? StatusId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Service> ServiceCreatedUsers { get; set; } = new List<Service>();

    public virtual ICollection<Service> ServiceFollowers { get; set; } = new List<Service>();

    public virtual ICollection<Service> ServiceUpdateUsers { get; set; } = new List<Service>();

    public virtual Status? Status { get; set; }
}
