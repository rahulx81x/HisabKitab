using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HisabKitabDAL.Models;

public partial class HisabKitabDbContext : DbContext
{
    public HisabKitabDbContext()
    {
    }

    public HisabKitabDbContext(DbContextOptions<HisabKitabDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");
        var config = builder.Build();
        var connectionString = config.GetConnectionString("HisabKitabDBConnectionString");

        if (!optionsBuilder.IsConfigured)
        {
           optionsBuilder.UseSqlServer(connectionString); 
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TranId).HasName("pk_tranId");

            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__UserI__3D5E1FD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("pk_userId");

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F284569A401933").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
