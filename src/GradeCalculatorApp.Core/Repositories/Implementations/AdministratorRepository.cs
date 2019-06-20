using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class AdministratorRepository : IAdministratorRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public AdministratorRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateAdministrator(Administrator administrator)
        {
            try
            {
                _gradeCalculatorContext.Administrators.Add(administrator);

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Administrator> ReadAdministrators(bool takeAll = true, int count = 1000)
        {
            try
            {
                return takeAll 
                    ? _gradeCalculatorContext.Administrators.Where(x => !x.IsDeleted && x.IsActive) 
                    : _gradeCalculatorContext.Administrators.Where(x => !x.IsDeleted && x.IsActive)
                        .Take(count);
            }
            catch (Exception e)
            {
                return new List<Administrator>();
            }
        }

        public Administrator ReadAdministrator(long administratorId)
        {
            try
            {
                return _gradeCalculatorContext.Administrators
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == administratorId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Administrator ReadAdministratorByEmail(string email)
        {
            try
            {
                return _gradeCalculatorContext.Administrators
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Email == email);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteAdministrator(long administratorId)
        {
            try
            {
                var administrator = _gradeCalculatorContext.Administrators.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == administratorId);

                if (administrator == null) return false;
                
                administrator.IsDeleted = true;
                administrator.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(administrator).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateAdministrator(long administratorId, Administrator administrator)
        {
            try
            {
                var currentAdministrator = _gradeCalculatorContext.Administrators.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == administratorId);

                if (currentAdministrator == null) return false;
                
                currentAdministrator.Email = administrator.Email;
                currentAdministrator.LastName = administrator.LastName;
                currentAdministrator.FirstName = administrator.FirstName;
                currentAdministrator.UserRole = administrator.UserRole;
                currentAdministrator.PasswordHash = administrator.PasswordHash;
                currentAdministrator.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentAdministrator).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}