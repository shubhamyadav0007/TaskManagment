using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskBackend.Models;

namespace TaskBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            builder.Entity<Models.Task>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict); 

            
            builder.Entity<Models.Task>()
                .HasMany(t => t.AssignedUsers)
                .WithMany(u => u.AssignedTasks)
                .UsingEntity<Dictionary<string, object>>(
                    "TaskAssignments",
                    j => j.HasOne<ApplicationUser>()
                          .WithMany()
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Models.Task>()
                          .WithMany()
                          .HasForeignKey("TaskId")
                          .OnDelete(DeleteBehavior.Cascade)
                );
            this.SeedUsersAndAdmin(builder);
        }
        private void SeedUsersAndAdmin(ModelBuilder builder)
        {
           
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "c0cd8ad0-04fe-4180-8b9c-c1466d14c2ec", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "d1371363-4642-4cf9-a99f-237b85eadd21", Name = "User", NormalizedName = "USER" }
            );

           
            var adminUserId = "a6b83fc3-370a-4d9b-8a02-7f7ed9bb6ae5";
            var regularUserId = "f3b0ae36-7c3b-4d48-9e7f-0b77e89a6f89"; 

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = "sy335740@gmail.com",
                    NormalizedUserName = "sy335740@GMAIL.COM",
                    Email = "sy335740@gmail.com",
                    NormalizedEmail = "sy335740@gmail.com",
                    OrganizationName = "Revocept",
                    PhoneNumber = "8950334790",
                    Name = "Shubham",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Admin@123")
                },
                new ApplicationUser
                {
                    Id = regularUserId,
                    UserName = "shubhamyy.aspirefox@gmail.com",
                    NormalizedUserName = "shubhamyy.aspirefox@gmail.com",
                    Email = "shubhamyy.aspirefox@gmail.com",
                    NormalizedEmail = "shubhamyy.aspirefox@gmail.com",
                    Name = "Shubham Yadav",
                    PhoneNumber = "8950334790",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "User@123")
                }
            );

            
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = "c0cd8ad0-04fe-4180-8b9c-c1466d14c2ec" },
                new IdentityUserRole<string> { UserId = regularUserId, RoleId = "d1371363-4642-4cf9-a99f-237b85eadd21" }
            );
        }

        public DbSet<Models.Task> Tasks { get; set; }
    }
}
