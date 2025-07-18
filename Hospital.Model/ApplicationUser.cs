using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Hospital.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public string Specialist { get; set; }
        public bool IsDoctor { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Appointment> DoctorAppointments { get; set; } 

        public ICollection<Appointment> PatientAppointments { get; set; } 
        public ICollection<PatientReport> PatientReportsAsDoctor { get; set; }

        public ICollection<PatientReport> PatientReportsAsPatient { get; set; } 
        public ICollection<Payroll> Payrolls { get; set; } 
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
