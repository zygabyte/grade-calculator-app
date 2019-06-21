using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class RegistrationCourseRepository : IRegistrationCourseRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public RegistrationCourseRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public IEnumerable<RegistrationCourse> ReadRegistrationCourses(long sessionSemesterId, long programmeId, long studentId)
        {
            try
            {
                var programmeCourseIds = _gradeCalculatorContext.ProgrammeCourses.
                    Include(x => x.Programme)
                    .Where(x => !x.IsDeleted && !x.Programme.IsDeleted && x.ProgrammeId == programmeId).Select(x => x.CourseId).ToList();
                var sessionSemesterCourseIds = _gradeCalculatorContext.SessionSemesterCourses
                    .Include(x => x.Course)
                    .Where(x => !x.IsDeleted && !x.Course.IsDeleted && x.SessionSemesterId == sessionSemesterId).Select(x => x.CourseId).ToList();
                var registeredCourseIds = _gradeCalculatorContext.RegisteredCourses  // registered courses for this student... we don't wanna add already registered courses
                    .Include(x => x.Student)
                    .Where(x => !x.IsDeleted && !x.Student.IsDeleted && x.StudentId == studentId).Select(x => x.CourseId).ToList();
                
                var registrationCourseIds = new List<long>();
                var registrationCourses = new List<RegistrationCourse>();
                
                Parallel.ForEach(programmeCourseIds,programmeCourseId =>
                { // as long as this course is in this session semester and it isn't yet registered... add it up
                    if (sessionSemesterCourseIds.Contains(programmeCourseId) && !registeredCourseIds.Contains(programmeCourseId)) registrationCourseIds.Add(programmeCourseId);
                });

                // for each course, who are all the lecturers teaching that course
                var lecturerIdsList = registrationCourseIds.Select(x =>
                    _gradeCalculatorContext.LecturerCourses
                        .Include(y => y.Course)
                        .Include(y => y.Lecturer)
                        .Where(y => !y.IsDeleted && !y.Course.IsDeleted && !y.Lecturer.IsDeleted && y.CourseId == x).ToList()).ToList();
              
                // for each course, loop through all lecturers to see who's teaching that particular course
                long id = 1;
                registrationCourseIds.ForEach(registrationCourseId =>
                {
                    lecturerIdsList.ForEach(lecturerIds =>
                    {
                        lecturerIds.ForEach(lecturerId =>
                        {
                            if (lecturerId.CourseId == registrationCourseId) // if the course this lecturer teaches is this registrationCourse, then add up
                            {
                                var lecturer = _gradeCalculatorContext.Lecturers.FirstOrDefault(x => !x.IsDeleted && x.Id == lecturerId.LecturerId);
                                var course = _gradeCalculatorContext.Courses.FirstOrDefault(x => !x.IsDeleted && x.Id == registrationCourseId);
                                
                                registrationCourses.Add(new RegistrationCourse
                                {
                                    Id = id++,
                                    Course = course?.Name,
                                    CourseCode = course?.Code,
                                    Lecturer = $"{lecturer?.FirstName} {lecturer?.LastName}",
                                    CourseId = course?.Id,
                                    LecturerId = lecturer?.Id,
                                    CourseUnit = course?.CreditUnit
                                });
                            }
                        });
                    });
                });

                return registrationCourses;
            }
            catch (Exception e)
            {
                return new List<RegistrationCourse>();
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}