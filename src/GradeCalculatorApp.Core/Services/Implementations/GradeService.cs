using System;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.EnumLibrary;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class GradeService : IGradeService
    {
        public RegisteredCourseGrade CalculateFinalGrade(RegisteredCourseGrade registeredCourseGrade)
        {
            try
            {
                CalculateTotalCa(registeredCourseGrade);
                CalculateTotalScore(registeredCourseGrade);
                CalculateGrade(registeredCourseGrade);

                return registeredCourseGrade;
            }
            catch (Exception e)
            {
                return registeredCourseGrade;
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