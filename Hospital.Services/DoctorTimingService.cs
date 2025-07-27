using cloudscribe.Pagination.Models;
using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Hospital.Services.Interfaces;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class DoctorTimingService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorTimingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddTiming(TimingViewModel timing)
        {
            var entity = timing.ConvertToModel();
            _unitOfWork.GetRepository<Timing>().Add(entity);
            _unitOfWork.Save();
        }

        public void UpdateTiming(TimingViewModel timing)
        {
            var entity = _unitOfWork.GetRepository<Timing>().GetById(timing.Id);
            if (entity == null) return;

            entity.Date = timing.SheduleDate;
            entity.MorningShiftStartTime = timing.MorningShiftStartTime;
            entity.MorningShiftEndTime = timing.MorningShiftEndTime;
            entity.AfternoonShiftStartTime = timing.AfternoonShiftStartTime;
            entity.AfternoonShiftEndTime = timing.AfternoonShiftEndTime;
            entity.Duration = timing.Duration;
            entity.Status = timing.Status;
            entity.DoctorId = timing.DoctorId;

            _unitOfWork.GetRepository<Timing>().Update(entity);
            _unitOfWork.Save();
        }

        public void DeleteTiming(int timingId)
        {
            var entity = _unitOfWork.GetRepository<Timing>().GetById(timingId);
            if (entity == null) return;

            _unitOfWork.GetRepository<Timing>().Delete(entity);
            _unitOfWork.Save();
        }

        public TimingViewModel GetTimingById(int timingId)
        {
            var entity = _unitOfWork.GetRepository<Timing>().GetById(timingId);
            return entity == null ? null : new TimingViewModel(entity);
        }

        public IEnumerable<TimingViewModel> GetAll()
        {
            return _unitOfWork.GetRepository<Timing>().GetAll()
                .Select(e => new TimingViewModel(e));
        }

        public PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize)
        {
            var all = _unitOfWork.GetRepository<Timing>().GetAll().ToList();

            var result = all
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new TimingViewModel(e))
                .ToList();

            return new PagedResult<TimingViewModel>
            {
                Data = result,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = all.Count
            };
        }
    }
}
