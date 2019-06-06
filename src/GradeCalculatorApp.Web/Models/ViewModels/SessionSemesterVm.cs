using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Web.Models.ViewModels
{
    public class SessionSemesterVm
    {
        public long Id { get; set; }
        public string Session { get; set; }
        public long SessionId { get; set; }
        public string Semester { get; set; }
        public long SemesterId { get; set; }
        public List<Course> Courses { get; set; }
        public string SemesterStartDate { get; set; }
        public string SemesterEndDate { get; set; }   
        public bool IsCurrent { get; set; }   
    }
}