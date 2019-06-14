namespace GradeCalculatorApp.Data.Domains
{
    public class Course : Unit
    {
        public int CreditUnit { get; set; }
//        public Lecturer Lecturer { get; set; }
//        public long LecturerId { get; set; }
        public LecturerCourse LecturerCourse { get; set; }
        public long LecturerCourseId { get; set; }
        public SessionSemesterCourse SessionSemesterCourse { get; set; }
        public long SessionSemesterCourseId { get; set; }
        public ProgrammeCourse ProgrammeCourse { get; set; }
        public long ProgrammeCourseId { get; set; }
//        public LecturerCourse LecturerCourse { get; set; }
//        public long LecturerCourseId { get; set; }
    }
}