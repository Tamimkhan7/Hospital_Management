using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

         public string PictureUri { get; set; }
        public Department Department { get; set; }
        [NotMapped]
        public ICollection<Appointment> DoctorAppointments { get; set; }
        [NotMapped]
        public ICollection<Payroll> Payrolls { get; set; }
        [NotMapped]
        public ICollection<Appointment> PatientAppointments { get; set; }
        [NotMapped]
        public ICollection<PatientReport> PatientReportsAsDoctor { get; set; }
        [NotMapped]
        public ICollection<PatientReport> PatientReportsAsPatient { get; set; } 

    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
