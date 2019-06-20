﻿// <auto-generated />
using System;
using GradeCalculatorApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GradeCalculatorApp.Data.Migrations
{
    [DbContext(typeof(GradeCalculatorContext))]
    partial class GradeCalculatorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Administrator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("PasswordHash");

                    b.Property<int>("UserRole");

                    b.HasKey("Id");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("Created");

                    b.Property<int>("CreditUnit");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Name");

                    b.Property<long>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Lecturer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long>("DepartmentId");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("PasswordHash");

                    b.Property<int>("UserRole");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.LecturerCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("LecturerId");

                    b.Property<DateTime?>("Modified");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("LecturerId");

                    b.ToTable("LecturerCourses");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Programme", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("Created");

                    b.Property<long>("DepartmentId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.ProgrammeCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<long>("ProgrammeId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProgrammeId");

                    b.ToTable("ProgrammeCourses");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.RegisteredCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("LecturerId");

                    b.Property<DateTime?>("Modified");

                    b.Property<long>("SessionSemesterId");

                    b.Property<long>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("LecturerId");

                    b.HasIndex("SessionSemesterId");

                    b.HasIndex("StudentId");

                    b.ToTable("RegisteredCourses");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.RegisteredCourseGrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Assignment1");

                    b.Property<int>("Assignment2");

                    b.Property<int>("Attendance");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Exam");

                    b.Property<int>("FinalScore");

                    b.Property<int>("Grade");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MidSemester");

                    b.Property<DateTime?>("Modified");

                    b.Property<int>("Project");

                    b.Property<int>("Quiz1");

                    b.Property<int>("Quiz2");

                    b.Property<long>("RegisteredCourseId");

                    b.Property<int>("TotalCa");

                    b.HasKey("Id");

                    b.HasIndex("RegisteredCourseId");

                    b.ToTable("RegisteredCourseGrades");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.School", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Semester", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Session", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.SessionSemester", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsCurrent");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<DateTime>("SemesterEndDate");

                    b.Property<long>("SemesterId");

                    b.Property<DateTime>("SemesterStartDate");

                    b.Property<long>("SessionId");

                    b.HasKey("Id");

                    b.HasIndex("SemesterId");

                    b.HasIndex("SessionId");

                    b.ToTable("SessionSemesters");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.SessionSemesterCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<long>("SessionSemesterId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("SessionSemesterId");

                    b.ToTable("SessionSemesterCourses");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<string>("MatricNumber");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("PasswordHash");

                    b.Property<long>("ProgrammeId");

                    b.Property<int>("UserRole");

                    b.HasKey("Id");

                    b.HasIndex("ProgrammeId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.TokenUserMap", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Token");

                    b.Property<int>("UserRole");

                    b.HasKey("Id");

                    b.ToTable("TokenUserMaps");
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Department", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.School", "School")
                        .WithMany("Departments")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Lecturer", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.Department", "Department")
                        .WithMany("Lecturers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.LecturerCourse", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.Course", "Course")
                        .WithMany("LecturerCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GradeCalculatorApp.Data.Domains.Lecturer", "Lecturer")
                        .WithMany("LecturerCourses")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Programme", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.Department", "Department")
                        .WithMany("Programmes")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.ProgrammeCourse", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.Course", "Course")
                        .WithMany("ProgrammeCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GradeCalculatorApp.Data.Domains.Programme", "Programme")
                        .WithMany("ProgrammeCourses")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.RegisteredCourse", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.Course", "Course")
                        .WithMany("RegisteredCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GradeCalculatorApp.Data.Domains.Lecturer", "Lecturer")
                        .WithMany("RegisteredCourses")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GradeCalculatorApp.Data.Domains.SessionSemester", "SessionSemester")
                        .WithMany("RegisteredCourses")
                        .HasForeignKey("SessionSemesterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GradeCalculatorApp.Data.Domains.Student", "Student")
                        .WithMany("RegisteredCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.RegisteredCourseGrade", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.RegisteredCourse", "RegisteredCourse")
                        .WithMany()
                        .HasForeignKey("RegisteredCourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.SessionSemester", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GradeCalculatorApp.Data.Domains.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.SessionSemesterCourse", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.Course", "Course")
                        .WithMany("SessionSemesterCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GradeCalculatorApp.Data.Domains.SessionSemester", "SessionSemester")
                        .WithMany("SessionSemesterCourses")
                        .HasForeignKey("SessionSemesterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GradeCalculatorApp.Data.Domains.Student", b =>
                {
                    b.HasOne("GradeCalculatorApp.Data.Domains.Programme", "Programme")
                        .WithMany("Students")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
