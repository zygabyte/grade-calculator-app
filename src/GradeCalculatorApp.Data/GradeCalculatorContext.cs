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

            // unique constraints on email
//            modelBuilder.Entity<Student>()
//                .HasIndex(x => x.Email)
//                .IsUnique();
//            
//            modelBuilder.Entity<Lecturer>()
//                .HasIndex(x => x.Email)
//                .IsUnique();
            
            modelBuilder.Entity<Programme>()
                .HasOne(p => p.Department)
                .WithMany(d => d.Programmes)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Lecturer>()
                .HasOne(l => l.Department)
                .WithMany(d => d.Lecturers)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Programme)
                .WithMany(p => p.Students)
                .OnDelete(DeleteBehavior.Restrict);
            
//            modelBuilder.Entity<Course>()
//                .HasOne(l => l.SessionSemesterCourse)
//                .WithMany(d => d.Course)
//                .OnDelete(DeleteBehavior.Restrict);
//            
//            modelBuilder.Entity<Course>()
//                .HasOne(l => l.LecturerCourse)
//                .WithMany(d => d.Course)
//                .OnDelete(DeleteBehavior.Restrict);
//            
//            modelBuilder.Entity<Course>()
//                .HasOne(l => l.ProgrammeCourse)
//                .WithMany(d => d.Course)
//                .OnDelete(DeleteBehavior.Restrict);


//            // session semester course
//            modelBuilder.Entity<SessionSemesterCourse>()
//                .HasKey(sc => new {sc.CourseId, sc.SessionSemesterId});

            modelBuilder.Entity<SessionSemesterCourse>()
                .HasOne(sc => sc.SessionSemester)
                .WithMany(s => s.SessionSemesterCourses)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SessionSemesterCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.SessionSemesterCourses)
                .OnDelete(DeleteBehavior.Restrict);

            
            //lecturer course
            modelBuilder.Entity<LecturerCourse>()
                .HasOne(lc => lc.Lecturer)
                .WithMany(l => l.LecturerCourses)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LecturerCourse>()
                .HasOne(lc => lc.Course)
                .WithMany(c => c.LecturerCourses)
                .OnDelete(DeleteBehavior.Restrict);

            
            // programme course
            modelBuilder.Entity<ProgrammeCourse>()
                .HasOne(pc => pc.Programme)
                .WithMany(p => p.ProgrammeCourses)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgrammeCourse>()
                .HasOne(pc => pc.Course)
                .WithMany(c => c.ProgrammeCourses)
                .OnDelete(DeleteBehavior.Restrict);


            // registered courses
            modelBuilder.Entity<RegisteredCourse>()
                .HasOne(rc => rc.Student)
                .WithMany(s => s.RegisteredCourses)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisteredCourse>()
                .HasOne(rc => rc.Lecturer)
                .WithMany(l => l.RegisteredCourses)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisteredCourse>()
                .HasOne(rc => rc.Course)
                .WithMany(c => c.RegisteredCourses)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisteredCourse>()
                .HasOne(rc => rc.SessionSemester)
                .WithMany(s => s.RegisteredCourses)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Server=.;Database=GradeCalculator;Trusted_Connection=True;MultipleActiveResultSets=true");
//        }

        public DbSet<SessionSemester> SessionSemesters { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        
        public DbSet<LecturerCourse> LecturerCourses { get; set; }
        public DbSet<ProgrammeCourse> ProgrammeCourses { get; set; }
        public DbSet<SessionSemesterCourse> SessionSemesterCourses { get; set; }
        
        public DbSet<RegisteredCourse> RegisteredCourses { get; set; }
        public DbSet<RegisteredCourseGrade> RegisteredCourseGrades { get; set; }
        
        public DbSet<TokenUserMap> TokenUserMaps { get; set; }
    }
}