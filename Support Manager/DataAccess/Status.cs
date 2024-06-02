using System;
using System.Collections.Generic;

namespace Support_Manager.DataAccess;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Follower> Followers { get; set; } = new List<Follower>();

    public virtual ICollection<Program> Programs { get; set; } = new List<Program>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
