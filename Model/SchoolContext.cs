using Microsoft.EntityFrameworkCore;
using ServiceSchool.Model;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    public DbSet<Students> Students { get; set; }
    public DbSet<Teachers> Teachers { get; set; }
    public DbSet<Courses> Courses { get; set; }
    public DbSet<SchoolGrades> SchoolGrades { get; set; }
    public DbSet<LogProcessDt> LogProcess { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Courses>().HasKey(c => c.IdCourse);
        modelBuilder.Entity<SchoolGrades>().HasKey(g => g.IdSchoolGrades);
        modelBuilder.Entity<Students>().HasKey(s => s.IdStudent);
        modelBuilder.Entity<Teachers>().HasKey(s => s.IdTeacher);
        modelBuilder.Entity<LogProcessDt>().HasKey(s => s.Hash);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Teachers>().HasData(
               new Teachers { IdTeacher = 1, NameTeacher = "Juan Pérez" }
           );

        modelBuilder.Entity<Courses>().HasData(
            new Courses { IdCourse = 1, NameCourse = "Matemáticas", AmountStudents = 0, IdTeacher = 1 }
        );

        modelBuilder.Entity<Students>().HasData(
            new Students
            {
                IdStudent = 1,
                NameStudent = "Ana",
                SurnameStuden = "García",
                NameGuardian = "Luis García",
                ContactGuardian = "555-1234",
                IdCourse = 1,
                DocumentNumberStudent = "12345678"
            }
        );
    }
}