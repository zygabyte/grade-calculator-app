using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class ProgrammeService : IProgrammeService
    {

        private readonly IProgrammeRepository _programmeRepository;
        
        public ProgrammeService(IProgrammeRepository programmeRepository) => _programmeRepository = programmeRepository;
        
        public bool CreateProgramme(Programme programme)
        {
            try
            {
                return programme != null && _programmeRepository.CreateProgramme(programme);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Programme> ReadProgrammes(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _programmeRepository.ReadProgrammes(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<Programme>();
            }
        }

        public Programme ReadProgramme(long programmeId)
        {
            try
            {
                return programmeId > 0 ? _programmeRepository.ReadProgramme(programmeId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteProgramme(long programmeId)
        {
            try
            {
                return programmeId > 0 && _programmeRepository.DeleteProgramme(programmeId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateProgramme(long programmeId, Programme programme)
        {
            try
            {
                return programmeId > 0 && programme != null && _programmeRepository.UpdateProgramme(programmeId, programme);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}