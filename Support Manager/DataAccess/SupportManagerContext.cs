using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Support_Manager.DataAccess;

public partial class SupportManagerContext : DbContext
{
    public SupportManagerContext()
    {
    }

    public SupportManagerContext(DbContextOptions<SupportManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Follower> Followers { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Support_Manager;Integrated Security=True;Encrypt=False;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.CaseId).HasName("PK_dbo_Case");

            entity.Property(e => e.CaseMessage).HasColumnType("text");
            entity.Property(e => e.CaseName).HasMaxLength(250);
            entity.Property(e => e.CaseNumber)
                .HasMaxLength(21)
                .HasComputedColumnSql("('C'+CONVERT([nvarchar](20),[CaseId]+(1000)))", false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedUser).WithMany(p => p.CaseCreatedUsers)
                .HasForeignKey(d => d.CreatedUserId)
                .HasConstraintName("FK_Cases_CreatedUser");

            entity.HasOne(d => d.Customer).WithMany(p => p.Cases)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Cases_Customer");

            entity.HasOne(d => d.Follower).WithMany(p => p.Cases)
                .HasForeignKey(d => d.FollowerId)
                .HasConstraintName("FK_Cases_Follower");

            entity.HasOne(d => d.Program).WithMany(p => p.Cases)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("FK_Cases_Program");

            entity.HasOne(d => d.Status).WithMany(p => p.Cases)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Cases_Status");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.CaseUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .HasConstraintName("FK_Cases_UpdateUser");

            entity.HasOne(d => d.User).WithMany(p => p.CaseUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Cases_Users");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(250);

            entity.HasOne(d => d.Follower).WithMany(p => p.Customers)
                .HasForeignKey(d => d.FollowerId)
                .HasConstraintName("FK_Customer_Follower");

            entity.HasOne(d => d.Status).WithMany(p => p.Customers)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Customer_Status");
        });

        modelBuilder.Entity<Follower>(entity =>
        {
            entity.HasKey(e => e.FollowerUserId);

            entity.ToTable("Follower");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Status).WithMany(p => p.Followers)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Follower_Status");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.ToTable("Program");

            entity.HasOne(d => d.Status).WithMany(p => p.Programs)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Program_Status");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK_dbo_Service");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ServiceMessage).HasColumnType("text");
            entity.Property(e => e.ServiceName).HasMaxLength(250);
            entity.Property(e => e.ServiceNumber)
                .HasMaxLength(21)
                .HasComputedColumnSql("('S'+CONVERT([nvarchar](20),[ServiceId]+(1000)))", false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedUser).WithMany(p => p.ServiceCreatedUsers)
                .HasForeignKey(d => d.CreatedUserId)
                .HasConstraintName("FK_Services_CreatedUser");

            entity.HasOne(d => d.Customer).WithMany(p => p.Services)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Services_Customer");

            entity.HasOne(d => d.Follower).WithMany(p => p.ServiceFollowers)
                .HasForeignKey(d => d.FollowerId)
                .HasConstraintName("FK_Services_Follower");

            entity.HasOne(d => d.Status).WithMany(p => p.Services)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Services_Status");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.ServiceUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .HasConstraintName("FK_Services_UpdateUser");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.UserName).HasMaxLength(250);
            entity.Property(e => e.UserTitle).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.Users)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Users_Customer");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Users_Status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
