using System;
using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GradeCalculatorApp.Data
{
    public class GradeCalculatorContext : DbContext
    {
        public GradeCalculatorContext(DbContextOptions contextOptions) : base(contextOptions)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Programme>()
                .HasOne(p => p.Department)
                .WithMany(d => d.Programmes);
            
            modelBuilder.Entity<Lecturer>()
                .HasOne(l => l.Department)
                .WithMany(d => d.Lecturers);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=GradeCalculator;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<SessionSemester> SessionSemesters { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        
        public DbSet<LecturerCourse> LecturerCourses { get; set; }
        public DbSet<ProgrammeCourse> ProgrammeCourses { get; set; }
        public DbSet<SessionSemesterCourse> SessionSemesterCourses { get; set; }
    }
}