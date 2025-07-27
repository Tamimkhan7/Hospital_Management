using cloudscribe.Pagination.Models;
using Hospital.Model;
using Hospital.Repositories.Implementation;
using Hospital.Repositories.Interfaces;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitofwork;

        public ApplicationUserService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize)
        {
            var repo = _unitofwork.GetRepository<ApplicationUser>();
            int totalCount = repo.GetAll().Count();

            int skip = (pageNumber - 1) * pageSize;
            var modelList = repo.GetAll()
                                .Skip(skip)
                                .Take(pageSize)
                                .ToList();

            var vmList = modelList
                         .Select(x => new ApplicationUserViewModel(x))
                         .ToList();

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public PagedResult<ApplicationUserViewModel> GetAllDoctors(int pageNumber, int pageSize)
        {
            var repo = _unitofwork.GetRepository<ApplicationUser>();
            var doctors = repo.GetAll().Where(x => x.IsDoctor == true);
            int totalCount = doctors.Count();

            int skip = (pageNumber - 1) * pageSize;
            var modelList = doctors
                                .Skip(skip)
                                .Take(pageSize)
                                .ToList();

            var vmList = modelList
                         .Select(x => new ApplicationUserViewModel(x))
                         .ToList();

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize 
            };
        }

        public PagedResult<ApplicationUserViewModel> GetAllPatient(int pageNumber, int pageSize)
        {
            var repo = _unitofwork.GetRepository<ApplicationUser>();
            var patients = repo.GetAll().Where(x => x.IsDoctor == false);
            int totalCount = patients.Count();

            int skip = (pageNumber - 1) * pageSize;
            var modelList = patients
                                .Skip(skip)
                                .Take(pageSize)
                                .ToList();

            var vmList = modelList
                         .Select(x => new ApplicationUserViewModel(x))
                         .ToList();

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public PagedResult<ApplicationUserViewModel> SearchDoctor(int pageNumber, int pageSize, string Specility = null)
        {
            var repo = _unitofwork.GetRepository<ApplicationUser>();
            var doctors = repo.GetAll().Where(x => x.IsDoctor == true);

            if (!string.IsNullOrEmpty(Specility))
            {
                doctors = doctors.Where(x => x.Specialist != null && x.Specialist.ToLower().Contains(Specility.ToLower()));
            }

            int totalCount = doctors.Count();

            int skip = (pageNumber - 1) * pageSize;
            var modelList = doctors
                                .Skip(skip)
                                .Take(pageSize)
                                .ToList();

            var vmList = modelList
                         .Select(x => new ApplicationUserViewModel(x))
                         .ToList();

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public ApplicationUserViewModel GetById(string id)
        {
            var repo = _unitofwork.GetRepository<ApplicationUser>();
            var user = repo.GetAll().FirstOrDefault(x => x.Id == id);
            if (user == null)
                return null;
            return new ApplicationUserViewModel(user);
        }

        public void CreateUser(ApplicationUserViewModel model)
        {
            var repo = _unitofwork.GetRepository<ApplicationUser>();
            var user = model.ConvertViewModelToModel(model);
            repo.Add(user);
            _unitofwork.Save();
        }

        public void UpdateUser(ApplicationUserViewModel model)
        {
            var repo = _unitofwork.GetRepository<ApplicationUser>();
            var user = repo.GetAll().FirstOrDefault(x => x.Id == model.Id);
            if (user != null)
            {
              
                user.Name = model.Name;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.City = model.City;
                user.Gender = model.Gender;
                user.IsDoctor = model.IsDoctor;
                user.Specialist = model.Specialist;              

                repo.Update(user);
                _unitofwork.Save();
            }
        }

        public void DeleteUser(string id)
        {
            var repo = _unitofwork.GetRepository<ApplicationUser>();
            var user = repo.GetAll().FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                repo.Delete(user);
                _unitofwork.Save();
            }
        }
    }
}
