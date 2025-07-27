using cloudscribe.Pagination.Models;
using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddTiming(TimingViewModel timing)
        {
            var entity = timing.ConvertToModel();  // এখন এইভাবে কল করবে
            _unitOfWork.GetRepository<Timing>().Add(entity);
            _unitOfWork.Save();
        }

        public void DeleteTiming(int timingId)
        {
            var model = _unitOfWork.GetRepository<Timing>().GetById(timingId);
            if (model != null)
            {
                _unitOfWork.GetRepository<Timing>().Delete(model);
                _unitOfWork.Save();
            }
        }

        public PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize)
        {
            var repo = _unitOfWork.GetRepository<Timing>();
            int totalCount = repo.GetAll().Count();
            var data = repo.GetAll()
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();

            var vmList = data.Select(x => new TimingViewModel(x)).ToList();

            return new PagedResult<TimingViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public IEnumerable<TimingViewModel> GetAll()
        {
            var list = _unitOfWork.GetRepository<Timing>().GetAll().ToList();
            return list.Select(x => new TimingViewModel(x)).ToList();
        }

        public TimingViewModel GetTimingById(int timingId)
        {
            var model = _unitOfWork.GetRepository<Timing>().GetById(timingId);
            return model == null ? null : new TimingViewModel(model);
        }

        public void UpdateTiming(TimingViewModel timing)
        {
            var repo = _unitOfWork.GetRepository<Timing>();
            var existing = repo.GetById(timing.Id);

            if (existing != null)
            {
                existing.Date = timing.SheduleDate;
                existing.MorningShiftStartTime = timing.MorningShiftStartTime;
                existing.MorningShiftEndTime = timing.MorningShiftEndTime;
                existing.AfternoonShiftStartTime = timing.AfternoonShiftStartTime;
                existing.AfternoonShiftEndTime = timing.AfternoonShiftEndTime;
                existing.Duration = timing.Duration;
                existing.Status = timing.Status;
                existing.DoctorId = timing.DoctorId;

                repo.Update(existing);
                _unitOfWork.Save();
            }
        }
    }
}
