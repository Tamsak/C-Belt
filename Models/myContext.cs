using Microsoft.EntityFrameworkCore;
 
namespace CSharpExam.Models
{
    public class myContext : DbContext
    {
        public myContext(DbContextOptions<myContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Activitycenter> Activitycenters { get; set; }
    }
}