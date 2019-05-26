using System;

namespace GradeCalculatorApp.Data.Domains
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        protected BaseEntity()
        {
            Created = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }
    }
}