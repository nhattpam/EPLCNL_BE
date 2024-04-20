using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Account
    {
        public Account()
        {
            Centers = new HashSet<Center>();
            Learners = new HashSet<Learner>();
            Salaries = new HashSet<Salary>();
            Tutors = new HashSet<Tutor>();
            Staff = new HashSet<Staff>();
        }

        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public Guid? RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Note { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Wallet? Wallet { get; set; }
        public virtual ICollection<Center> Centers { get; set; }
        public virtual ICollection<Learner> Learners { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<Tutor> Tutors { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
