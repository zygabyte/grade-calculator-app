using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class GradeRepository : IGradeRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public GradeRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public IEnumerable<GradedCourse> ReadGradedCourses(long sessionSemesterId, long studentId)
        {
            try
            {
                var gradedCourses =_gradeCalculatorContext.RegisteredCourseGrades
                    .Include(x => x.RegisteredCourse).Include(x => x.RegisteredCourse.Course)
                    .Include(x => x.RegisteredCourse.Lecturer)
                    .Where(x => !x.IsDeleted && !x.RegisteredCourse.IsDeleted &&
                                !x.RegisteredCourse.Course.IsDeleted && !x.RegisteredCourse.Lecturer.IsDeleted &&
                                x.RegisteredCourse.SessionSemesterId == sessionSemesterId &&
                                x.RegisteredCourse.StudentId == studentId);

                var gradedCourseList = new  List<GradedCourse>();
                foreach (var gradedCourse in gradedCourses)
                {
                    var registeredCourse = gradedCourse.RegisteredCourse;
                    var lecturer = registeredCourse.Lecturer;
                    
                    gradedCourseList.Add(new GradedCourse
                    {
                        Id = registeredCourse.Id,
                        Course = registeredCourse.Course.Name,
                        CourseId = registeredCourse.CourseId,
                        CourseCode = registeredCourse.Course.Code,
                        CourseUnit = registeredCourse.Course.CreditUnit,
                        Lecturer = $"{lecturer.FirstName} {lecturer.LastName}",
                        LecturerId = registeredCourse.LecturerId,
                        Grade = gradedCourse.Grade.ToString()
                    });    
                }

                return gradedCourseList;
            }
            catch (Exception e)
            {
                return new List<GradedCourse>();
            }
        }

        public RegisteredCourseGrade ReadGradedCourse(long gradedCourseId)
        {
            try
            {
                return _gradeCalculatorContext.RegisteredCourseGrades.FirstOrDefault(x =>
                    !x.IsDeleted && x.RegisteredCourseId == gradedCourseId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}