using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext()
        { }

        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AuctionDb;Trusted_Connection=True;");
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Lot> Lots { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LotConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoConfiguration());
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Surname).IsRequired().HasMaxLength(60);
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(60);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(50);

            builder.HasMany(c => c.Orders).WithOne(t => t.User);
        }
    }

    public class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(150);
            builder.Property(p => p.StartDate).HasColumnType("DateTime").IsRequired();
            builder.Property(p => p.EndDate).HasColumnType("DateTime").IsRequired();

            builder.HasMany(p => p.OrderDetails).WithOne(p => p.Lot);
        }
    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(r => r.OrderDetails).WithOne(r => r.Order);
        }
    }

    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(et => et.GroupOfPhoto).IsRequired();
        }
    }

}
