using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public Gender? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Description { get; set; }
        public string? Mobile { get; set; }
        public DateTime? DOB { get; set; }
        public string? Specialist { get; set; }
        public bool? IsDoctor { get; set; }
        public string? PictureUri { get; set; }
        public Department? Department { get; set; }

        // ✅ Proper mapped relationships (remove [NotMapped])
        [NotMapped]
        public virtual ICollection<Appointment> DoctorAppointments { get; set; } = new List<Appointment>();
        [NotMapped]
        public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
        [NotMapped]
        public virtual ICollection<PatientReport> PatientReportsAsDoctor { get; set; } = new List<PatientReport>();
        [NotMapped]
        public virtual ICollection<PatientReport> PatientReportsAsPatient { get; set; } = new List<PatientReport>();
        [NotMapped]
        public virtual ICollection<PatientReport> PatientAppointments { get; set; } = new List<PatientReport>();
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
