using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using magnadigi.Areas.Identity.Data;
using magnadigi.Models;


namespace magnadigi.Data;

public class magnadigiContext : IdentityDbContext<magnadigiUser>
{
    public magnadigiContext(DbContextOptions<magnadigiContext> options)
        : base(options)
    {
    }

    //public object TaskModel { get; internal set; }
    public DbSet<magnadigi.Models.TaskModel>? TaskModel { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<magnadigiUser>
{
    public void Configure(EntityTypeBuilder<magnadigiUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
        builder.Property(u => u.BusinessName).HasMaxLength(255);
        builder.Property(u => u.PhoneNumber).HasMaxLength(255);
    }

}




    

    

