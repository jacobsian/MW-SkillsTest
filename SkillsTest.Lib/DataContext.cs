using Microsoft.EntityFrameworkCore;
using SkillsTest.Lib;

namespace SkillsTest.Lib
{
    public class DataContext : DbContext
    {
        public DbSet<Student> Students { get; private set; } = null!; 

        public DbSet<Course> Courses { get; private set; } = null!;

        public DataContext(DbContextOptions options) : base(options)
        {
            // Nothing to do here
        }
    }
}