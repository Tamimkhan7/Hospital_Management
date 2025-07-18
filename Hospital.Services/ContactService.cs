using Hospital.Model;
using Hospital.Repositories.Interfaces;
using cloudscribe.Pagination.Models;
using Hospital.ViewModels;
using System.Linq;

namespace Hospital.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteContact(int id)
        {
            var repo = _unitOfWork.GetRepository<Contact>();
            var model = repo.GetById(id);
            if (model != null)
            {
                repo.Delete(model);
                _unitOfWork.Save();
            }
        }

        public PagedResult<ContactViewModel> GetAll(int pageNumber, int pageSize)
        {
            var repo = _unitOfWork.GetRepository<Contact>();
            int totalCount = repo.GetAll().Count();

            var modelList = repo.GetAll()
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            var vmList = modelList
                         .Select(x => new ContactViewModel(x))
                         .ToList();

            return new PagedResult<ContactViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public ContactViewModel GetContactById(int contactId)
        {
            var repo = _unitOfWork.GetRepository<Contact>();
            var model = repo.GetById(contactId);
            return model != null ? new ContactViewModel(model) : null;
        }

        public void InsertContact(ContactViewModel contact)
        {
            var repo = _unitOfWork.GetRepository<Contact>();
            var model = new ContactViewModel().ConvertViewModel(contact);
            repo.Add(model);
            _unitOfWork.Save();
        }

        public void UpdateContact(ContactViewModel contact)
        {
            var repo = _unitOfWork.GetRepository<Contact>();
            var updated = new ContactViewModel().ConvertViewModel(contact);

            var existing = repo.GetById(updated.Id);
            if (existing != null)
            {
                existing.Phone = updated.Phone;
                existing.Email = updated.Email;
                existing.HospitalId = updated.HospitalId;

                repo.Update(existing);
                _unitOfWork.Save();
            }
        }
    }
}
