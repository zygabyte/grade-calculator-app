namespace GradeCalculatorApp.Data.Models
{
    public class DashboardModel
    {
        public double StudentCgpa { get; set; }
        public int StudentTotalRegisteredCourses { get; set; }
        
        public int LecturerTotalCourses { get; set; }
        public int LecturerTotalRegisteredCourses { get; set; }
        public int LecturerTotalStudents { get; set; }

        public int AdminTotalSchools { get; set; }
        public int AdminTotalDepartments { get; set; }
        public int AdminTotalProgrammes { get; set; }
        public int AdminTotalCourses { get; set; }
        public int AdminTotalSessionSemester { get; set; }
        public int AdminTotalLecturers { get; set; }
        public int AdminTotalStudents { get; set; }
    }
}