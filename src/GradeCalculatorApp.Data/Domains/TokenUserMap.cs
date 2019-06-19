using System;
using GradeCalculatorApp.EnumLibrary;

namespace GradeCalculatorApp.Data.Domains
{
    public class TokenUserMap : BaseEntity
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }
}