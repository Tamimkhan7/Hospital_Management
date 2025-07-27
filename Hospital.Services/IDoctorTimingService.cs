using cloudscribe.Pagination.Models;
using Hospital.ViewModels;

namespace Hospital.Services.Interfaces
{
    public interface IDoctorService
    {
        PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize);
        TimingViewModel GetTimingById(int id);
        void AddTiming(TimingViewModel model);
        void UpdateTiming(TimingViewModel model);
        void DeleteTiming(int id);
    }
}