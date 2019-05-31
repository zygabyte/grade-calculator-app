using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ISemesterService
    {
        bool CreateSemester(Semester semester);
        IEnumerable<Semester> ReadSemesters(bool takeAll = true, int count = 1000);
        Semester ReadSemester(long semesterId);
        bool DeleteSemester(long semesterId);
        bool UpdateSemester(long semesterId, Semester semester);
    }
}