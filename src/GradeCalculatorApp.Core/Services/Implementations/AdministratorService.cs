using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IMailService _mailService;
        
        public AdministratorService(IAdministratorRepository administratorRepository, IMailService mailService)
        {
            _administratorRepository = administratorRepository;
            _mailService = mailService;
        }

        public bool CreateAdministrator(Administrator administrator)
        {
            try
            {
                var tokenMap = Guid.NewGuid().ToString().Replace("-", "0");
                if (administrator != null && _administratorRepository.CreateAdministrator(administrator))
                {
                    _mailService.SendRegisterMail(administrator.Email, $"{administrator.FirstName} {administrator.LastName}", administrator.UserRole.ToString(), tokenMap);
                    return true;
                }

                return false;
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
                return _administratorRepository.ReadAdministrators(takeAll, count);
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
                return administratorId > 0 ? _administratorRepository.ReadAdministrator(administratorId) : null;
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
                return administratorId > 0 && _administratorRepository.DeleteAdministrator(administratorId);
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
                return administratorId > 0 && administrator != null && _administratorRepository.UpdateAdministrator(administratorId, administrator);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}