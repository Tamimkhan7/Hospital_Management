using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteRoom(int id)
        {
            var repo = _unitOfWork.GetRepository<Room>();
            var model = repo.GetById(id);
            if (model != null)
            {
                repo.Delete(model);
                _unitOfWork.Save();
            }
        }

        public PagedResult<RoomViewModel> GetAll(int pageNumber, int pageSize)
        {
            var repo = _unitOfWork.GetRepository<Room>();

            int totalCount = repo.GetAll().Count();

            int skip = (pageNumber - 1) * pageSize;
            var modelList = repo.GetAll()
                                .Skip(skip)
                                .Take(pageSize)
                                .ToList();

            var vmList = modelList
                         .Select(x => new RoomViewModel(x))
                         .ToList();

            return new PagedResult<RoomViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public RoomViewModel GetRoomById(int roomId)
        {
            var repo = _unitOfWork.GetRepository<Room>();
            var model = repo.GetById(roomId);
            return model == null ? null : new RoomViewModel(model);
        }

        public void InsertRoom(RoomViewModel room)
        {
            var repo = _unitOfWork.GetRepository<Room>();
            var model = new RoomViewModel().ConvertViewModel(room);
            repo.Add(model);
            _unitOfWork.Save();
        }

        public void UpdateRoom(RoomViewModel room)
        {
            var repo = _unitOfWork.GetRepository<Room>();
            var updated = new RoomViewModel().ConvertViewModel(room);

            var existing = repo.GetById(updated.Id);
            if (existing != null)
            {
                existing.RoomNumber = updated.RoomNumber;
                existing.Type = updated.Type;
                existing.Status = updated.Status;
                existing.HospitalId = room.HospitalInfoId;

                repo.Update(existing);
                _unitOfWork.Save();
            }
        }
    }
}
