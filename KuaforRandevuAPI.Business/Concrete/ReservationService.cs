using AutoMapper;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Reservation;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KuaforRandevuAPI.Business.Concrete
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IBarberRepository _barberRepository;
        private readonly IMapper _mapper;
        public ReservationService(IRepository<Reservation> repository, IMapper mapper, IReservationRepository reservationRepository, IBarberRepository barberRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _barberRepository = barberRepository;
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
        public async Task<List<ResultReservationDto>> GetReservationForToday()
        {
            var reservations = await _reservationRepository.GetReservationsForToday();
            return _mapper.Map<List<ResultReservationDto>>(reservations);
        }
        public async Task Create(CreateReservationDto dto)
        {
            bool checkReservationAvailable = await CheckReservationAvailabble(dto);
            if (true)
            {
                var available = await GetAvailableReservation(dto.BarberId, dto.Date);
                if (available != null)
                {
                    //await _repository.Add(_mapper.Map<Reservation>(dto));
                }
            }
            else
            {
            }
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
        public async Task<bool> CheckReservationAvailabble(CreateReservationDto dto)
        {
            var reservationList = await GetAllReservations();
            foreach (var reservation in reservationList)
            {
                if (reservation != null)
                {
                    if (reservation.BarberId == dto.BarberId)
                    {
                        var reservationBarber = await _barberRepository.GetBarberByIdWithServices(3);
                        if (reservationBarber != null)
                        {
                            // Saat Kontrolü
                            if (reservationBarber.StartTime < dto.Time && reservationBarber.EndTime > dto.Time)
                            {
                                // Mesai saati kontrolü başarılı.
                                return true;
                            }
                            else
                            {
                                // Berberin mesai saati dışında bir istek.
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public async Task<string> GetAvailableReservation(int barberId, DateOnly minDate)
        {
            if (barberId != 0)
            {
                Barber? barber = await _barberRepository.GetBarberByIdWithServices(barberId);
                if (barber != null)
                {
                    var reservations = await _reservationRepository.GetReservationsForToday();

                    TimeOnly jobStartTime = barber.StartTime; // Mesai başlangıç saati
                    TimeOnly jobEndTime = barber.EndTime; // Mesai bitiş saati
                    TimeSpan totalWorkTimeSpan = jobEndTime - jobStartTime; // Kaç saat çalışıyor bu berber?

                    TimeOnly currentHour = jobStartTime;
                    int foundReservation = 0;
                    int totalWorkHours = Convert.ToInt32(totalWorkTimeSpan.TotalHours);
                    Dictionary<int, string> findingReservations = new Dictionary<int, string>(); // Testing

                    List<string> doluSaatler = new List<string>();
                    List<string> bulunanSaatler = new List<string>();

                    /* 
                     * 2 ile çarpmamızın sebebi hem o güne hemde o günden bir sonraki güne bakması içindir
                     * mesela bir berberin mesai saati 7 saat ise sonraki güne de bakacağı için 14 kez dönmesi gerekir döngünün.
                     * 
                     * 
                     * 20:00 + 40 = 20:40
                     * 20:35
                    */

                    // Günlük Döngüsü
                    int j = 0;
                    for (int i = 1; i <= totalWorkHours; i++)
                    {
                        if (currentHour >= jobEndTime)
                        {
                            break;
                        }

                        if (!reservations.Any(x => x.Time == currentHour))
                        {
                            findingReservations.Add(i, currentHour.ToString());
                            bulunanSaatler.Add(currentHour.ToString());
                        }
                        else
                        {
                            doluSaatler.Add(currentHour.ToString());
                        }
                        currentHour = currentHour.AddHours(1);
                    }
                    foreach (var item in findingReservations)
                    {
                        Console.WriteLine(item.Value);
                    }
                }
            }
            return "Selamun Aleyküm";
        }
    }
}
