using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IAdministratorService
    {
        bool CreateAdministrator(Administrator lecturer);
        IEnumerable<Administrator> ReadAdministrators(bool takeAll = true, int count = 1000);
        Administrator ReadAdministrator(long lecturerId);
        bool DeleteAdministrator(long lecturerId);
        bool UpdateAdministrator(long lecturerId, Administrator lecturer);
    }
}