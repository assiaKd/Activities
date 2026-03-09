using Activities.Domain;
using Microsoft.EntityFrameworkCore;
using Activity = Activities.Domain.Activity;

namespace Activities.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public required DbSet<Activity> Activities { get; set; }
        public required DbSet<ActivityAttendee> ActivityAttendees { get; set; }
        public required DbSet<Photo> Photos { get; set; }
        public required DbSet<Comment> Comments { get; set; }
        public required DbSet<UserFollowing> UserFollowings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserFollowing>()
                .HasKey(uf => new { uf.ObserverId, uf.TargetId });

            builder.Entity<UserFollowing>()
                .HasOne(uf => uf.Observer)
                .WithMany(u => u.Followings)
                .HasForeignKey(uf => uf.ObserverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFollowing>()
                .HasOne(uf => uf.Target)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.TargetId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<ActivityAttendee>()
       .HasKey(aa => new { aa.UserId, aa.ActivityId });

            // Relationships
            builder.Entity<ActivityAttendee>()
                .HasOne(aa => aa.User)
                .WithMany(u => u.Activities)
                .HasForeignKey(aa => aa.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ActivityAttendee>()
                .HasOne(aa => aa.Activity)
                .WithMany(a => a.Attendees)
                .HasForeignKey(aa => aa.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
