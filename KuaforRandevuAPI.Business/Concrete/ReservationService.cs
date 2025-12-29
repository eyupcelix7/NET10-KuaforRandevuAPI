using AutoMapper;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Reservation;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Concrete
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        public ReservationService(IRepository<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<ResultReservationDto>> GetAllReservations()
        {
            var reservations = await _repository.GetAll();
            return _mapper.Map<List<ResultReservationDto>>(reservations);
        }
        public async Task<ResultReservationDto> GetReservationById(int id)
        {
            var reservation = await _repository.GetById(id);
            return _mapper.Map<ResultReservationDto>(reservation);
        }
        public Task<List<ResultReservationDto>> GetReservationForToday()
        {
            // Özel Repository ile yazılacak.
            throw new NotImplementedException();
        }
        public async Task Create(CreateReservationDto dto)
        {
            await _repository.Add(_mapper.Map<Reservation>(dto));
        }
        public async Task Update(UpdateReservationDto dto)
        {
            var updatedReservation = await _repository.GetById(dto.Id);
            if (updatedReservation != null)
            {
                updatedReservation.Name = dto.Name;
                updatedReservation.Status = dto.Status;
                updatedReservation.PhoneNumber = dto.PhoneNumber;
                updatedReservation.BarberId = dto.BarberId;
                updatedReservation.Date = dto.Date;
                await _repository.Update(updatedReservation);
            }
        }
        public async Task Remove(int id)
        {
            var service = await _repository.GetById(id);
            if (service != null)
            {
                await _repository.Remove(service);
            }
        }
    }
}
