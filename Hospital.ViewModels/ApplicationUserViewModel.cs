using Hospital.Model;
using System;
using System.Collections.Generic;

namespace Hospital.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Nationality { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Description { get; set; }
        public string? Mobile { get; set; }
        public DateTime? DOB { get; set; }
        public string? Specialist { get; set; }
        public bool? IsDoctor { get; set; }
        public string? PictureUri { get; set; }
        public Gender? Gender { get; set; }
        public Department? Department { get; set; }

        public List<ApplicationUser> Doctors { get; set; } = new List<ApplicationUser>();

        public ApplicationUserViewModel() { }

        public ApplicationUserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            UserName = user.UserName;
            Nationality = user.Nationality;
            Address = user.Address;
            City = user.City;
            Description = user.Description;
            Mobile = user.Mobile;
            DOB = user.DOB;
            Specialist = user.Specialist;
            IsDoctor = user.IsDoctor;
            PictureUri = user.PictureUri;
            Gender = user.Gender;
            Department = user.Department;
        }

        public ApplicationUser ConvertViewModelToModel(ApplicationUserViewModel user)
        {
            return new ApplicationUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                UserName = user.UserName,
                Nationality = user.Nationality,
                Address = user.Address,
                City = user.City,
                Description = user.Description,
                Mobile = user.Mobile,
                DOB = user.DOB,
                Specialist = user.Specialist,
                IsDoctor = user.IsDoctor,
                PictureUri = user.PictureUri,
                Gender = user.Gender,
                Department = user.Department
            };
        }
    }
}
