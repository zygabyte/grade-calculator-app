using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IAdministratorRepository
    {
        bool CreateAdministrator(Administrator administrator);
        IEnumerable<Administrator> ReadAdministrators(bool takeAll = true, int count = 1000);
        Administrator ReadAdministrator(long administratorId);
        Administrator ReadAdministratorByEmail(string email);
        bool DeleteAdministrator(long administratorId);
        bool UpdateAdministrator(long administratorId, Administrator administrator);
    }
}