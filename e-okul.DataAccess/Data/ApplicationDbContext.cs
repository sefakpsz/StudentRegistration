using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using e_okul.Models;

namespace e_okul.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<LectureName> LectureNames { get; set; }
    }
}