using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hospital.ViewModels
{
    public class TimingViewModel
    {
        public int Id { get; set; }
        public DateTime SheduleDate { get; set; }
        public int MorningShiftStartTime { get; set; }
        public int MorningShiftEndTime { get; set; }
        public int AfternoonShiftStartTime { get; set; }
        public int AfternoonShiftEndTime { get; set; }
        public Status Status { get; set; }
        public int Duration { get; set; }
        public Guid? DoctorId { get; set; }

        public List<SelectListItem> mornignShiftStart { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> mornignShiftEnd { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AfternoonShiftStart { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AfternoonShiftEnd { get; set; } = new List<SelectListItem>();

        public ApplicationUser Doctor { get; set; }

        public TimingViewModel() { }

        public TimingViewModel(Timing model)
        {
            Id = model.Id;
            SheduleDate = model.Date ?? DateTime.MinValue;
            MorningShiftStartTime = model.MorningShiftStartTime ?? 0;
            MorningShiftEndTime = model.MorningShiftEndTime ?? 0;
            AfternoonShiftStartTime = model.AfternoonShiftStartTime ?? 0;
            AfternoonShiftEndTime = model.AfternoonShiftEndTime ?? 0;
            Duration = model.Duration ?? 0;
            Status = model.Status ?? Status.Available;
            Doctor = model.Doctor;
            DoctorId = model.DoctorId;
        }

        public Timing ConvertToModel()
        {
            return new Timing
            {
                Id = this.Id,
                Date = this.SheduleDate,
                MorningShiftStartTime = this.MorningShiftStartTime,
                MorningShiftEndTime = this.MorningShiftEndTime,
                AfternoonShiftStartTime = this.AfternoonShiftStartTime,
                AfternoonShiftEndTime = this.AfternoonShiftEndTime,
                Duration = this.Duration,
                Status = this.Status,
                Doctor = this.Doctor,
                DoctorId = this.DoctorId
            };
        }
    }

}
