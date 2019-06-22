using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class CourseService : ICourseService
    {

        private readonly ICourseRepository _courseRepository;
        
        public CourseService(ICourseRepository courseRepository) => _courseRepository = courseRepository;
        
        public bool CreateCourse(Course course)
        {
            try
            {
                return course != null && _courseRepository.CreateCourse(course);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Course> ReadCourses(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _courseRepository.ReadCourses(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }

        public Course ReadCourse(long courseId)
        {
            try
            {
                return courseId > 0 ? _courseRepository.ReadCourse(courseId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteCourse(long courseId)
        {
            try
            {
                return courseId > 0 && _courseRepository.DeleteCourse(courseId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateCourse(long courseId, Course course)
        {
            try
            {
                return courseId > 0 && course != null && _courseRepository.UpdateCourse(courseId, course);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UploadCourses(IFormFile formFile)
        {
            try
            {
                var courses = new List<Course>();
               
                if (formFile != null && formFile.Length > 0)
                {
                    Console.WriteLine("file name " + formFile.FileName);

                    XSSFWorkbook excelUpload;
                    using (var excelStream = formFile.OpenReadStream())
                        excelUpload = new XSSFWorkbook(excelStream);

                    var sheet = excelUpload.GetSheet("Courses");

                    for (var row = 1; row <= sheet.LastRowNum; row++) // iterating through rows
                    {
                        var course = new Course
                        {
                            Name = sheet.GetRow(row).GetCell(0).StringCellValue,
                            Code = sheet.GetRow(row).GetCell(1).StringCellValue,
                            CreditUnit = Convert.ToInt32(sheet.GetRow(row).GetCell(2).NumericCellValue)
                        };
                        courses.Add(course);
                    }
                    
                    return _courseRepository.CreateCourses(courses);
                }

                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public FileModel DownloadCourseTemplate(string filename = "CoursesTemplate.xlsx")
        {
            try
            {  
                var path = Path.Combine(  
                    DirectoryConstants.BaseTemplatesDir, filename);  
  
                var memory = new MemoryStream();  
                using (var stream = new FileStream(path, FileMode.Open)) stream.CopyTo(memory);  
                memory.Position = 0;  
                
                return new FileModel { MemoryStream = memory, ContentType = GetContentType(path), Path = path };
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new FileModel();
            }
        }
        
        private static string GetContentType(string path)  
        {  
            var types = GetMimeTypes();  
            var ext = Path.GetExtension(path).ToLowerInvariant();  
            return types[ext];  
        }
        
        private static Dictionary<string, string> GetMimeTypes()  
        {  
            return new Dictionary<string, string>  
            {  
                {".txt", "text/plain"},  
                {".pdf", "application/pdf"},  
                {".doc", "application/vnd.ms-word"},  
                {".docx", "application/vnd.ms-word"},  
                {".xls", "application/vnd.ms-excel"},  
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},  
                {".jpg", "image/jpeg"},  
                {".jpeg", "image/jpeg"},  
                {".gif", "image/gif"},  
                {".csv", "text/csv"}  
            };  
        } 
        
//        public bool MapCourseToSessionSemesterCourse(long sessionSemesterCourseId, List<long> courseIds)
//        {
//            try
//            {
//                Parallel.ForEach(courseIds, courseId => _courseRepository.MapCourseToSessionSemesterCourse(sessionSemesterCourseId, courseId));
//
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }
//
//        public bool MapCourseToProgrammeCourse(long programmeCourseId, List<long> courseIds)
//        {
//            try
//            {
//                Parallel.ForEach(courseIds, courseId => _courseRepository.MapCourseToProgrammeCourse(programmeCourseId, courseId));
//
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }
//
//        public bool MapCourseToLecturerCourse(long lecturerCourseId, List<long> courseIds)
//        {
//            try
//            {
//                Parallel.ForEach(courseIds, courseId => _courseRepository.MapCourseToLecturerCourse(lecturerCourseId, courseId));
//
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }
    }
}