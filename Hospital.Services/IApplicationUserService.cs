using cloudscribe.Pagination.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IApplicationUserService
    {
        PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize);
        PagedResult<ApplicationUserViewModel> GetAllDoctors(int pageNumber, int pageSize);
        PagedResult<ApplicationUserViewModel> GetAllPatient(int pageNumber, int pageSize);
        PagedResult<ApplicationUserViewModel> SearchDoctor(int pageNumber, int pageSize, string Specility= null);
        ApplicationUserViewModel GetById(string id);
        void CreateUser(ApplicationUserViewModel model);
        void UpdateUser(ApplicationUserViewModel model);
        void DeleteUser(string id);
    }
}
