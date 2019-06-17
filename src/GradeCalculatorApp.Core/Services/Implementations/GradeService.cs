using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using GradeCalculatorApp.EnumLibrary;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        
        public GradeService(IGradeRepository gradeRepository) => _gradeRepository = gradeRepository;
        
        public void CalculateFinalGrade(RegisteredCourseGrade registeredCourseGrade)
        {
            try
            {
                CalculateTotalCa(registeredCourseGrade);
                CalculateTotalScore(registeredCourseGrade);
                CalculateGrade(registeredCourseGrade);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public IEnumerable<GradedCourse> ReadGradedCourses(long sessionSemesterId, long studentId)
        {
            try
            {
                return _gradeRepository.ReadGradedCourses(sessionSemesterId, studentId);
            }
            catch (Exception e)
            {
                return new List<GradedCourse>();
            }
        }

        public RegisteredCourseGradeModel ReadGradedCourse(long gradedCourseId)
        {
            try
            {
                var gradedCourse = _gradeRepository.ReadGradedCourse(gradedCourseId);
                return new RegisteredCourseGradeModel
                {
                    Exam = gradedCourse.Exam, Quiz1 = gradedCourse.Quiz1, Quiz2 = gradedCourse.Quiz2,
                    Assignment1 = gradedCourse.Assignment1, Assignment2 = gradedCourse.Assignment2,
                    Project = gradedCourse.Project, Attendance = gradedCourse.Attendance,
                    TotalCa = gradedCourse.TotalCa,
                    FinalScore = gradedCourse.FinalScore, MidSemester = gradedCourse.MidSemester,
                    Grade = gradedCourse.Grade.ToString()
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static void CalculateTotalCa(RegisteredCourseGrade registeredCourseGrade)
        {
            try
            {
                registeredCourseGrade.TotalCa = registeredCourseGrade.Quiz1 + registeredCourseGrade.Quiz2 +
                                                registeredCourseGrade.Assignment1 + registeredCourseGrade.Assignment2
                                                + registeredCourseGrade.Attendance + registeredCourseGrade.MidSemester +
                                                registeredCourseGrade.Project;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void CalculateTotalScore(RegisteredCourseGrade registeredCourseGrade)
        {
            try
            {
                registeredCourseGrade.FinalScore = registeredCourseGrade.TotalCa + registeredCourseGrade.Exam;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void CalculateGrade(RegisteredCourseGrade registeredCourseGrade)
        {
            try
            {
                if (registeredCourseGrade.FinalScore >= 0 && registeredCourseGrade.FinalScore <= 39) registeredCourseGrade.Grade = Grade.F;
                else if (registeredCourseGrade.FinalScore >= 40 && registeredCourseGrade.FinalScore <= 44) registeredCourseGrade.Grade = Grade.E;
                else if (registeredCourseGrade.FinalScore >= 45 && registeredCourseGrade.FinalScore <= 49) registeredCourseGrade.Grade = Grade.D;
                else if (registeredCourseGrade.FinalScore >= 50 && registeredCourseGrade.FinalScore <= 59) registeredCourseGrade.Grade = Grade.C;
                else if (registeredCourseGrade.FinalScore >= 60 && registeredCourseGrade.FinalScore <= 79) registeredCourseGrade.Grade = Grade.B;
                else if (registeredCourseGrade.FinalScore >= 80 && registeredCourseGrade.FinalScore <= 100) registeredCourseGrade.Grade = Grade.A;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}