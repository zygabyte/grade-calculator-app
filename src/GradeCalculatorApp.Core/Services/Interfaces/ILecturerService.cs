using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Http;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ILecturerService
    {
        bool CreateLecturer(Lecturer lecturer);
        IEnumerable<Lecturer> ReadLecturers(bool takeAll = true, int count = 1000);
        Lecturer ReadLecturer(long lecturerId);
        Lecturer ReadLecturerByEmail(string email);
        bool DeleteLecturer(long lecturerId);
        bool UpdateLecturer(long lecturerId, Lecturer lecturer);
        ResponseData UploadLecturers(IFormFile formFile);
        FileModel DownloadLecturerTemplate(string filename = "LecturersTemplate.xlsx");
    }
}