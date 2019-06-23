using System;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProgrammeRepository _programmeRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISessionSemesterRepository _sessionSemesterRepository;
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IStudentRepository _studentRepository;
        
        private readonly ILecturerCourseRepository _lecturerCourseRepository;
        private readonly IRegisteredCourseRepository _registeredCourseRepository;
        public DashboardService(ISchoolRepository schoolRepository, IDepartmentRepository departmentRepository, IProgrammeRepository programmeRepository,
            ICourseRepository courseRepository, ILecturerRepository lecturerRepository, IStudentRepository studentRepository, ISessionSemesterRepository sessionSemesterRepository,
            ILecturerCourseRepository lecturerCourseRepository, IRegisteredCourseRepository registeredCourseRepository)
        {
            _schoolRepository = schoolRepository;
            _departmentRepository = departmentRepository;
            _programmeRepository = programmeRepository;
            _courseRepository = courseRepository;
            _lecturerRepository = lecturerRepository;
            _studentRepository = studentRepository;
            _sessionSemesterRepository = sessionSemesterRepository;
            _lecturerCourseRepository = lecturerCourseRepository;
            _registeredCourseRepository = registeredCourseRepository;
        }
        
        public DashboardModel GetAdminDashboard()
        {
            try
            {
                return new DashboardModel
                {
                    AdminTotalSchools = _schoolRepository.CountTotalSchools(),
                    AdminTotalDepartments = _departmentRepository.CountTotalDepartments(),
                    AdminTotalProgrammes = _programmeRepository.CountTotalProgrammes(),
                    AdminTotalCourses = _courseRepository.CountTotalCourses(),
                    AdminTotalSessionSemester = _sessionSemesterRepository.CountTotalSessionSemesters(),
                    AdminTotalLecturers = _lecturerRepository.CountTotalLecturers(),
                    AdminTotalStudents = _studentRepository.CountTotalStudents()
                };
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public DashboardModel GetLecturerDashboard(long sessionSemesterId, long lecturerId)
        {
            try
            {
                return new DashboardModel
                {
                    LecturerTotalCourses = _lecturerCourseRepository.CountTotalLecturerCourses(lecturerId),
                    LecturerTotalStudents = _registeredCourseRepository.CountTotalLecturerStudents(sessionSemesterId, lecturerId),
                    LecturerTotalRegisteredCourses = _registeredCourseRepository.CountTotalLecturerRegisteredCourses(sessionSemesterId, lecturerId)
                };
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public DashboardModel GetStudentDashboard()
        {
            throw new System.NotImplementedException();
        }
    }
}