using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ISemesterRepository
    {
        bool CreateSemester(Semester semester);
        IEnumerable<Semester> ReadSemesters(bool takeAll = true, int count = 1000);
        Semester ReadSemester(long semesterId);
        bool DeleteSemester(long semesterId);
        bool UpdateSemester(long semesterId, Semester semester);
    }
}