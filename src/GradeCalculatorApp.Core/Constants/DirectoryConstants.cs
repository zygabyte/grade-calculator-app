using System;
using System.IO;

namespace GradeCalculatorApp.Core.Constants
{
    public class DirectoryConstants
    {
        private static string BaseDir { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
        public static string BaseTemplatesDir { get; } = Path.Combine(BaseDir, "wwwroot", "Templates");
    }
}