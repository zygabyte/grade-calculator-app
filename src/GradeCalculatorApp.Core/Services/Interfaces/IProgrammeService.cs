using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IProgrammeService
    {
        bool CreateProgramme(Programme programme);
        IEnumerable<Programme> ReadProgrammes(bool takeAll = true, int count = 1000);
        Programme ReadProgramme(long programmeId);
        bool DeleteProgramme(long programmeId);
        bool UpdateProgramme(long programmeId, Programme programme);
    }
}