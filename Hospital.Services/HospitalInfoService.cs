using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class HospitalInfoService : IHospitalInfo
    {
        private readonly IUnitOfWork _unitOfWork;

        public HospitalInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteHospitalInfo(int id)
        {
            var repo = _unitOfWork.GetRepository<HospitalInfo>();
            var model = repo.GetById(id);
            if (model != null)
            {
                repo.Delete(model);
                _unitOfWork.Save();
            }
        }

        public PagedResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
            var repo = _unitOfWork.GetRepository<HospitalInfo>();


            int totalCount = repo.GetAll().Count();

      
            int skip = (pageNumber - 1) * pageSize;
            var modelList = repo.GetAll()
                                .Skip(skip)
                                .Take(pageSize)
                                .ToList();

            var vmList = modelList
                         .Select(x => new HospitalInfoViewModel(x))
                         .ToList();

            return new PagedResult<HospitalInfoViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public HospitalInfoViewModel GetHospitalById(int hospitalId)
        {
            var repo = _unitOfWork.GetRepository<HospitalInfo>();
            var model = repo.GetById(hospitalId);
            return model == null ? null : new HospitalInfoViewModel(model);
        }

        public void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var repo = _unitOfWork.GetRepository<HospitalInfo>();
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            repo.Add(model);
            _unitOfWork.Save();
        }

        public void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var repo = _unitOfWork.GetRepository<HospitalInfo>();
            var updated = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);

            var existing = repo.GetById(updated.Id);
            if (existing != null)
            {
                existing.Name = updated.Name;
                existing.Type = updated.Type;
                existing.City = updated.City;
                existing.PinCode = updated.PinCode;
                existing.Country = updated.Country;

                repo.Update(existing);
                _unitOfWork.Save();
            }
        }
    }
}
