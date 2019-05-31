using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        bool CreateStudent(Student student);
        IEnumerable<Student> ReadStudents(bool takeAll = true, int count = 1000);
        Student ReadStudent(long studentId);
        bool DeleteStudent(long studentId);
        bool UpdateStudent(long studentId, Student student);
    }
}