using cloudscribe.Pagination.Models;
using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface IContactService
    {
        PagedResult<ContactViewModel> GetAll(int pageNumber, int pageSize);
        ContactViewModel GetContactById(int contactId);
        void InsertContact(ContactViewModel contact);
        void UpdateContact(ContactViewModel contact);
        void DeleteContact(int id);

        // ✅ FIXED RETURN TYPE
        IEnumerable<HospitalInfoViewModel> GetHospitalInfos();
    }
}
