using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IStudentService
    {
        bool CreateStudent(Student student);
        IEnumerable<Student> ReadStudents(bool takeAll = true, int count = 1000);
        Student ReadStudent(long studentId);
        Student ReadStudentByEmail(string email);
        bool DeleteStudent(long studentId);
        bool UpdateStudent(long studentId, Student student);
    }
}