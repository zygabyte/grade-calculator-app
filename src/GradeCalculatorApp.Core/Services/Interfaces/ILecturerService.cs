using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ILecturerService
    {
        bool CreateLecturer(Lecturer lecturer);
        IEnumerable<Lecturer> ReadLecturers(bool takeAll = true, int count = 1000);
        Lecturer ReadLecturer(long lecturerId);
        bool DeleteLecturer(long lecturerId);
        bool UpdateLecturer(long lecturerId, Lecturer lecturer);
    }
}