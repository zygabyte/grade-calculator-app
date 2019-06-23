using System;
using System.Collections.Generic;
using System.IO;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Core.Utilities;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IProgrammeRepository _programmeRepository;
        private readonly IMailService _mailService;
        
        public StudentService(IStudentRepository studentRepository, IMailService mailService, IProgrammeRepository programmeRepository)
        {
            _studentRepository = studentRepository;
            _mailService = mailService;
            _programmeRepository = programmeRepository;
        }

        public bool CreateStudent(Student student)
        {
            try
            {
                var tokenMap = Guid.NewGuid().ToString().Replace("-", "0");
                if (student != null && _studentRepository.CreateStudent(student, tokenMap))
                {
                    _mailService.SendRegisterMail(student.Email, $"{student.FirstName} {student.LastName}", student.UserRole.ToString(), tokenMap);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Student> ReadStudents(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _studentRepository.ReadStudents(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<Student>();
            }
        }

        public Student ReadStudent(long studentId)
        {
            try
            {
                return studentId > 0 ? _studentRepository.ReadStudent(studentId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        public Student ReadStudentByEmail(string email)
        {
            try
            {
                return _studentRepository.ReadStudentByEmail(email);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteStudent(long studentId)
        {
            try
            {
                return studentId > 0 && _studentRepository.DeleteStudent(studentId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateStudent(long studentId, Student student)
        {
            try
            {
                return studentId > 0 && student != null && _studentRepository.UpdateStudent(studentId, student);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        
        public ResponseData UploadStudents(IFormFile formFile)
        {
            try
            {
                if (formFile != null && formFile.Length > 0)
                {
                    Console.WriteLine("file name " + formFile.FileName);
                    
                    XSSFWorkbook excelUpload;
                    using (var excelStream = formFile.OpenReadStream())
                        excelUpload = new XSSFWorkbook(excelStream);

                    var programme = _programmeRepository.ReadDefaultProgramme();

                    if (programme.Id == 0) return ResponseData.SendFailMsg("No programme exist yet. Kindly create at least one");

                    var sheet = excelUpload.GetSheet("Students");

                    for (var row = 1; row <= sheet.LastRowNum; row++) // iterating through rows
                    {
                        if (!CreateStudent(new Student
                        {
                            FirstName = sheet.GetRow(row).GetCell(0).StringCellValue,
                            LastName = sheet.GetRow(row).GetCell(1).StringCellValue,
                            Email = sheet.GetRow(row).GetCell(2).StringCellValue,
                            MatricNumber = sheet.GetRow(row).GetCell(3).StringCellValue,
                            ProgrammeId = programme.Id
                        })) return ResponseData.SendFailMsg("Attempted to upload an Invalid student. Kindly retry with valid students");
                    }

                    return ResponseData.SendSuccessMsg();
                }

                return ResponseData.SendFailMsg("Invalid upload file supplied");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return ResponseData.SendFailMsg("Error in uploading students. Kindly retry with valid students");
            }
        }

        public FileModel DownloadStudentTemplate(string filename = "StudentsTemplate.xlsx")
        {
            try
            {  
                var path = Path.Combine(  
                    DirectoryConstants.BaseTemplatesDir, filename);  
  
                var memory = new MemoryStream();  
                using (var stream = new FileStream(path, FileMode.Open)) stream.CopyTo(memory);  
                memory.Position = 0;  
                
                return new FileModel { MemoryStream = memory, ContentType = DirectoryUtility.GetContentType(path), Path = path };
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new FileModel();
            }
        }
    }
}