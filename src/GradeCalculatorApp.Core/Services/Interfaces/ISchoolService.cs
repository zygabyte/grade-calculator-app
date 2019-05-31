using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ISchoolService
    {
        bool CreateSchool(School school);
        IEnumerable<School> ReadSchools(bool takeAll = true, int count = 1000);
        School ReadSchool(long schoolId);
        bool DeleteSchool(long schoolId);
        bool UpdateSchool(long schoolId, School school);
    }
}