using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ILecturerRepository
    {
        bool CreateLecturer(Lecturer lecturer);
        IEnumerable<Lecturer> ReadLecturers(bool takeAll = true, int count = 1000);
        Lecturer ReadLecturer(long lecturerId);
        Lecturer ReadLecturerByEmail(string email);
        bool DeleteLecturer(long lecturerId);
        bool UpdateLecturer(long lecturerId, Lecturer lecturer);
    }
}