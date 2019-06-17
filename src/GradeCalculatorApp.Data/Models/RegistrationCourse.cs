using GradeCalculatorApp.EnumLibrary;

namespace GradeCalculatorApp.Data.Models
{
    public class RegistrationCourse : CourseLecturer
    {}

    public class GradedCourse : CourseLecturer
    {
        public string Grade { get; set; }
    }
    
    public class CourseLecturer
    {
        public long Id { get; set; }
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public int? CourseUnit { get; set; }
        public long? CourseId { get; set; }
        public string Lecturer { get; set; }
        public long? LecturerId { get; set; }
    }
}