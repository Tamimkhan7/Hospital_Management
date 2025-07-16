using Hospital.Utilities;
using Hospital.ViewModels;

namespace Hospital.Services
{
    public interface IHospitalInfo
    {
        PagedResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize);
        HospitalInfoViewModel GetHospitalById(int hospitalId);
        void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo);
        void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo);
        void DeleteHospitalInfo(int id);
    }
}
