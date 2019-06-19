using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        bool CreateStudent(Student student, string tokenMap);
        IEnumerable<Student> ReadStudents(bool takeAll = true, int count = 1000);
        Student ReadStudent(long studentId);
        Student ReadStudentByEmail(string email);
        bool DeleteStudent(long studentId);
        bool UpdateStudent(long studentId, Student student);
    }
}