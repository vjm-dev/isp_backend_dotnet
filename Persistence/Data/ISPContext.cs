using ISP_Backend_Dotnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISP_Backend_Dotnet.Persistence.Data
{
    public class ISPContext : DbContext
    {
        public ISPContext(DbContextOptions<ISPContext> options) : base(options)
        {
        }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DataUsage> DataUsages { get; set; }
        public DbSet<DataConsumption> DataConsumptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan>().ToTable("plans");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<DataUsage>().ToTable("data_usages");
            modelBuilder.Entity<DataConsumption>().ToTable("daily_usages");

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(p => p.Id).HasColumnName("id");
                entity.Property(p => p.Name).HasColumnName("name");
                entity.Property(p => p.Speed).HasColumnName("speed");
                entity.Property(p => p.MonthlyPayment).HasColumnName("monthly_payment");
                entity.Property(p => p.DataLimit).HasColumnName("data_limit");
                entity.Property(p => p.CreatedAt).HasColumnName("created_at");
                entity.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.Name).HasColumnName("name");
                entity.Property(u => u.Email).HasColumnName("email");
                entity.Property(u => u.Phone).HasColumnName("phone");
                entity.Property(u => u.Password).HasColumnName("password");
                entity.Property(u => u.PlanId).HasColumnName("plan_id");
                entity.Property(u => u.LastUpdated).HasColumnName("last_updated");
            });

            modelBuilder.Entity<DataUsage>(entity =>
            {
                entity.Property(du => du.Id).HasColumnName("id");
                entity.Property(du => du.StartDate).HasColumnName("start_date");
                entity.Property(du => du.EndDate).HasColumnName("end_date");
                entity.Property(du => du.Used).HasColumnName("used");
                entity.Property(du => du.Limit).HasColumnName("limit");
                entity.Property(du => du.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<DataConsumption>(entity =>
            {
                entity.Property(dc => dc.Id).HasColumnName("id");
                entity.Property(dc => dc.Date).HasColumnName("date");
                entity.Property(dc => dc.Download).HasColumnName("download");
                entity.Property(dc => dc.Upload).HasColumnName("upload");
                entity.Property(dc => dc.DataUsageId).HasColumnName("data_usage_id");
            });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Plan)
                .WithMany()
                .HasForeignKey(u => u.PlanId);

            modelBuilder.Entity<DataUsage>()
                .HasOne(du => du.User)
                .WithOne(u => u.DataUsage)
                .HasForeignKey<DataUsage>(du => du.UserId);

            modelBuilder.Entity<DataConsumption>()
                .HasOne(dc => dc.DataUsage)
                .WithMany(du => du.DailyUsage)
                .HasForeignKey(dc => dc.DataUsageId);
        }
    }
}