using System.IO;

namespace GradeCalculatorApp.Data.Models
{
    public class FileModel
    {
        public MemoryStream MemoryStream { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
    }
}