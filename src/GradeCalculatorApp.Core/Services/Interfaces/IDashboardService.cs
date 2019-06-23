using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IDashboardService
    {
        DashboardModel GetAdminDashboard();
        DashboardModel GetLecturerDashboard(long sessionSemesterId, long lecturerId);
        DashboardModel GetStudentDashboard();
    }
}