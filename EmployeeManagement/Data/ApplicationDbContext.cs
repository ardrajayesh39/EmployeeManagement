using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(
              new Department { DepartmentId = 1, DepartmentName = "HR" },
              new Department { DepartmentId = 2, DepartmentName = "IT" },
              new Department { DepartmentId = 3, DepartmentName = "Finance" }
);


            // ================= EMPLOYEE =================
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeName)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsRequired();

            // ================= EMPLOYEE SKILL =================
            modelBuilder.Entity<EmployeeSkill>()
                .HasKey(es => new { es.EmployeeId, es.SkillId });

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(es => es.Employees)
                .WithMany(e => e.EmployeeSkills)
                .HasForeignKey(es => es.EmployeeId);

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(es => es.Skill)
                .WithMany(s => s.EmployeeSkills)
                .HasForeignKey(es => es.SkillId);

            // ================= SKILL =================
            modelBuilder.Entity<Skill>()
                .Property(s => s.SkillName)
                .IsRequired();

            modelBuilder.Entity<Skill>()
                .HasIndex(s => s.SkillName)
                .IsUnique();
        }
    }
}
