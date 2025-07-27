using cloudscribe.Pagination.Models;
using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface IDoctorService
    {
        PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize);
        IEnumerable<TimingViewModel> GetAll();
        TimingViewModel GetTimingById(int timingId);
        void AddTiming(TimingViewModel timing);
        void UpdateTiming(TimingViewModel timing);
        void DeleteTiming(int timingId);
    }
}
