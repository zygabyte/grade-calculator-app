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
    public class LecturerService : ILecturerService
    {

        private readonly ILecturerRepository _lecturerRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMailService _mailService;
        
        public LecturerService(ILecturerRepository lecturerRepository, IMailService mailService, IDepartmentRepository departmentRepository)
        {
            _lecturerRepository = lecturerRepository;
            _mailService = mailService;
            _departmentRepository = departmentRepository;
        }

        public bool CreateLecturer(Lecturer lecturer)
        {
            try
            {
                var tokenMap = Guid.NewGuid().ToString().Replace("-", "0");
                if (lecturer != null && _lecturerRepository.CreateLecturer(lecturer, tokenMap))
                {
                    _mailService.SendRegisterMail(lecturer.Email, $"{lecturer.FirstName} {lecturer.LastName}", lecturer.UserRole.ToString(), tokenMap);
                    return true;
                }

                return false;
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
                return _lecturerRepository.ReadLecturers(takeAll, count);
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
                return lecturerId > 0 ? _lecturerRepository.ReadLecturer(lecturerId) : null;
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
                return _lecturerRepository.ReadLecturerByEmail(email);
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
                return lecturerId > 0 && _lecturerRepository.DeleteLecturer(lecturerId);
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
                return lecturerId > 0 && lecturer != null && _lecturerRepository.UpdateLecturer(lecturerId, lecturer);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        
        public ResponseData UploadLecturers(IFormFile formFile)
        {
            try
            {
                if (formFile != null && formFile.Length > 0)
                {
                    Console.WriteLine("file name " + formFile.FileName);
                    
                    XSSFWorkbook excelUpload;
                    using (var excelStream = formFile.OpenReadStream())
                        excelUpload = new XSSFWorkbook(excelStream);

                    var department = _departmentRepository.ReadDefaultDepartment();

                    if (department.Id == 0) return ResponseData.SendFailMsg("No department exist yet. Kindly create at least one");

                    var sheet = excelUpload.GetSheet("Lecturers");

                    for (var row = 1; row <= sheet.LastRowNum; row++) // iterating through rows
                    {
                        if (!CreateLecturer(new Lecturer
                        {
                            FirstName = sheet.GetRow(row).GetCell(0).StringCellValue,
                            LastName = sheet.GetRow(row).GetCell(1).StringCellValue,
                            Email = sheet.GetRow(row).GetCell(2).StringCellValue,
                            DepartmentId = department.Id
                        })) return ResponseData.SendFailMsg("Attempted to upload an Invalid lecturer. Kindly retry with valid lecturers");
                    }

                    return ResponseData.SendSuccessMsg();
                }

                return ResponseData.SendFailMsg("Invalid upload file supplied");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return ResponseData.SendFailMsg("Error in uploading lecturers. Kindly retry with valid lecturers");
            }
        }

        public FileModel DownloadLecturerTemplate(string filename = "LecturersTemplate.xlsx")
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