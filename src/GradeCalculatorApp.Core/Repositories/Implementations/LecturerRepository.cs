using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class LecturerRepository : ILecturerRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public LecturerRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateLecturer(Lecturer lecturer, string tokenMap)
        {
            try
            {
                _gradeCalculatorContext.Lecturers.Add(lecturer);
                _gradeCalculatorContext.TokenUserMaps.Add(new TokenUserMap
                {
                    Email = lecturer.Email, UserRole = lecturer.UserRole, Token = tokenMap
                });

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Lecturer> ReadLecturers(bool takeAll = true, int count = 1000)
        {
            try
            {
                return takeAll 
                    ? _gradeCalculatorContext.Lecturers.Where(x => !x.IsDeleted && x.IsActive) 
                        .Include(x => x.Department)
                    : _gradeCalculatorContext.Lecturers.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x => x.Department)
                        .Take(count);
            }
            catch (Exception e)
            {
                return new List<Lecturer>();
            }
        }

        public Lecturer ReadLecturer(long lecturerId)
        {
            try
            {
                return _gradeCalculatorContext.Lecturers
                    .Include(x => x.Department)
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == lecturerId && !x.Department.IsDeleted);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Lecturer ReadLecturerByEmail(string email)
        {
            try
            {
                return _gradeCalculatorContext.Lecturers
                    .Include(x => x.Department)
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Email == email && !x.Department.IsDeleted);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteLecturer(long lecturerId)
        {
            try
            {
                var lecturer = _gradeCalculatorContext.Lecturers.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == lecturerId);

                if (lecturer == null) return false;
                
                lecturer.IsDeleted = true;
                lecturer.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(lecturer).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateLecturer(long lecturerId, Lecturer lecturer)
        {
            try
            {
                var currentLecturer = _gradeCalculatorContext.Lecturers.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == lecturerId);

                if (currentLecturer == null) return false;
                
                currentLecturer.Department = lecturer.Department;
                currentLecturer.DepartmentId = lecturer.DepartmentId;
                currentLecturer.Email = lecturer.Email;
                currentLecturer.LastName = lecturer.LastName;
                currentLecturer.FirstName = lecturer.FirstName;
                currentLecturer.UserRole = lecturer.UserRole;
                currentLecturer.PasswordHash = lecturer.PasswordHash;
                currentLecturer.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentLecturer).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public int CountTotalLecturers()
        {
            try
            {
                return _gradeCalculatorContext.Lecturers.Count(x => !x.IsDeleted);
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}