using Hospital.Model;
using Hospital.Repositories.Interfaces;
using cloudscribe.Pagination.Models;
using Hospital.ViewModels;
using System.Collections.Generic;
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
            var hospitalRepo = _unitOfWork.GetRepository<HospitalInfo>();

            var allContacts = repo.GetAll().ToList();
            var totalCount = allContacts.Count;

            var pagedContacts = allContacts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var contactVMs = pagedContacts.Select(contact =>
            {
                var vm = new ContactViewModel(contact);
                var hospital = hospitalRepo.GetById(contact.HospitalId);
                if (hospital != null)
                {
                    vm.Name = hospital.Name;
                    vm.City = hospital.City;
                    vm.Country = hospital.Country;
                    vm.HospitalInfoId = hospital.Id;
                }
                return vm;
            }).ToList();

            return new PagedResult<ContactViewModel>
            {
                Data = contactVMs,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public ContactViewModel GetContactById(int contactId)
        {
            var repo = _unitOfWork.GetRepository<Contact>();
            var hospitalRepo = _unitOfWork.GetRepository<HospitalInfo>();

            var contact = repo.GetById(contactId);
            if (contact == null) return null;

            var vm = new ContactViewModel(contact);
            var hospital = hospitalRepo.GetById(contact.HospitalId);
            if (hospital != null)
            {
                vm.Name = hospital.Name;
                vm.City = hospital.City;
                vm.Country = hospital.Country;
                vm.HospitalInfoId = hospital.Id;
            }

            return vm;
        }

        public void InsertContact(ContactViewModel vm)
        {
            var repo = _unitOfWork.GetRepository<Contact>();
            var model = vm.ConvertViewModel();
            repo.Add(model);
            _unitOfWork.Save();
        }

        public void UpdateContact(ContactViewModel vm)
        {
            var repo = _unitOfWork.GetRepository<Contact>();
            var existing = repo.GetById(vm.Id);

            if (existing != null)
            {
                existing.Phone = vm.Phone;
                existing.Email = vm.Email;
                existing.HospitalId = vm.HospitalInfoId;

                repo.Update(existing);
                _unitOfWork.Save();
            }
        }

        // ✅ FIXED METHOD BELOW
        public IEnumerable<HospitalInfoViewModel> GetHospitalInfos()
        {
            var hospitalRepo = _unitOfWork.GetRepository<HospitalInfo>();
            var hospitals = hospitalRepo.GetAll().ToList();

            return hospitals.Select(h => new HospitalInfoViewModel
            {
                Id = h.Id,
                Name = h.Name
            });
        }
    }
}
