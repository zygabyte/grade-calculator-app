using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IProgrammeRepository
    {
        bool CreateProgramme(Programme programme);
        IEnumerable<Programme> ReadProgrammes(bool takeAll = true, int count = 1000);
        Programme ReadProgramme(long programmeId);
        Programme ReadDefaultProgramme();
        bool DeleteProgramme(long programmeId);
        bool UpdateProgramme(long programmeId, Programme programme);
        
        int CountTotalProgrammes();
    }
}