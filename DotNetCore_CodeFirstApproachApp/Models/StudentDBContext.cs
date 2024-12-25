using Microsoft.EntityFrameworkCore;

namespace DotNetCore_CodeFirstApproachApp.Models
{
    public class StudentDBContext:DbContext
    {
        public StudentDBContext(DbContextOptions<StudentDBContext> options):base(options)  
        {
            

        }

        public DbSet<Student> Students { get; set; }
    }
}
