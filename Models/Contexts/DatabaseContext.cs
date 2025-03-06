using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ComputerRepairService.Models.Contexts;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<JobPart> JobParts { get; set; }

    public virtual DbSet<JobService> JobServices { get; set; }

    public virtual DbSet<JobStatus> JobStatuses { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<PartCategory> PartCategories { get; set; }

    public virtual DbSet<PartOrder> PartOrders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<RepairJob> RepairJobs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // 1️⃣ Ustawia bazową ścieżkę na katalog, w którym działa aplikacja
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // 2️⃣ Wczytuje plik appsettings.json
            .Build(); // 3️⃣ Tworzy obiekt konfiguracji

        string connectionString = config.GetConnectionString("DefaultConnection"); // 4️⃣ Pobiera connection string

        optionsBuilder.UseSqlServer(connectionString); // 5️⃣ Konfiguruje połączenie do bazy SQL Server
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Addresse__3214EC07CDDCF804");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07322360B2");

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Addre__398D8EEE");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0782E3CB50");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees).HasConstraintName("FK__Employees__RoleI__3E52440B");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC07AC1039E9");

            entity.HasOne(d => d.Job).WithMany(p => p.Feedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__JobId__6477ECF3");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoices__3214EC07C84C7120");

            entity.HasOne(d => d.Job).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__JobId__5BE2A6F2");
        });

        modelBuilder.Entity<JobPart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobParts__3214EC076A07993C");

            entity.HasOne(d => d.Job).WithMany(p => p.JobParts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobParts__JobId__52593CB8");

            entity.HasOne(d => d.Part).WithMany(p => p.JobParts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobParts__PartId__534D60F1");
        });

        modelBuilder.Entity<JobService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobServi__3214EC0761B6D160");

            entity.HasOne(d => d.Job).WithMany(p => p.JobServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobServic__JobId__5812160E");

            entity.HasOne(d => d.Service).WithMany(p => p.JobServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobServic__Servi__59063A47");
        });

        modelBuilder.Entity<JobStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobStatu__3214EC073F596421");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Parts__3214EC076578FE94");

            entity.HasOne(d => d.PartCategory).WithMany(p => p.Parts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Parts__PartCateg__4316F928");
        });

        modelBuilder.Entity<PartCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PartCate__3214EC0724AA271B");
        });

        modelBuilder.Entity<PartOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PartOrde__3214EC07047AC5F3");

            entity.HasOne(d => d.Part).WithMany(p => p.PartOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PartOrder__PartI__49C3F6B7");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PartOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PartOrder__Suppl__48CFD27E");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC074D8EA0CF");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Invoic__60A75C0F");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Paymen__619B8048");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC074CD80D85");
        });

        modelBuilder.Entity<RepairJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RepairJo__3214EC07523B0235");

            entity.HasOne(d => d.Customer).WithMany(p => p.RepairJobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RepairJob__Custo__4E88ABD4");

            entity.HasOne(d => d.JobStatusNavigation).WithMany(p => p.RepairJobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RepairJob__JobSt__4F7CD00D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07EE00C430");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schedule__3214EC0737EF3E91");

            entity.HasOne(d => d.Employee).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedules__Emplo__6754599E");

            entity.HasOne(d => d.Job).WithMany(p => p.Schedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedules__JobId__68487DD7");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Services__3214EC078F28E172");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC07E9F855D5");

            entity.HasOne(d => d.Address).WithMany(p => p.Suppliers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Suppliers__Addre__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
