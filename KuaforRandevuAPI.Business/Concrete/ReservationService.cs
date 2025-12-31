using AutoMapper;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Reservation;
using KuaforRandevuAPI.Entities.Concrete;
using KuaforRandevuAPI.Entities.Enums.Reservation;
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
        private readonly IServiceRepository _serviceRepository;
        private readonly IBarberRepository _barberRepository;
        private readonly IMapper _mapper;
        public ReservationService(IRepository<Reservation> repository, IMapper mapper, IReservationRepository reservationRepository, IBarberRepository barberRepository, IServiceRepository serviceRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _barberRepository = barberRepository;
            _serviceRepository = serviceRepository;
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
        public async Task<List<ResultReservationDto>> GetReservationByBarberId(ReservationStatus status, int barberId)
        {
            var reservations = await _reservationRepository.GetReservationsByBarberId(status, barberId);
            return _mapper.Map<List<ResultReservationDto>>(reservations);
        }
        public async Task Create(CreateReservationDto dto)
        {
            bool hourAvailable = await CheckHourAvailable(dto); // Mesai saatleri kontrolü
            if (hourAvailable)
            {
                var reservationAvailable = await CheckReservationAvailable(dto);

                if (reservationAvailable)
                {
                    //await _repository.Add(_mapper.Map<Reservation>(dto));
                }
                else
                {
                    var suggestions = await GetReservationSuggestions(dto);
                    foreach (var item in suggestions)
                    {
                        Console.WriteLine(item);
                    }
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
        public async Task<bool> CheckHourAvailable(CreateReservationDto dto)
        {
            var reservationList = await GetAllReservations();
            foreach (var reservation in reservationList)
            {
                if (reservation != null)
                {
                    if (reservation.BarberId == dto.BarberId)
                    {
                        var reservationBarber = await _barberRepository.GetBarberByIdWithServices(dto.BarberId);
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
        public async Task<bool> CheckReservationAvailable(CreateReservationDto dto)
        {
            var reservations = await _reservationRepository.GetReservationsByBarberId(ReservationStatus.Pending,dto.BarberId);
            var services = await _serviceRepository.GetServicesByBarberId(dto.BarberId);
            var selectedService = services.Where(x => x.Id == dto.ServiceId).FirstOrDefault(); // Seçilen hizmetin süresine erişmek için !

            var reservationsForDate = reservations.Where(x=> x.Date == dto.Date);

            var isAvailable = !reservationsForDate.Any(y =>
            {
                var reservationStartTime = y.Time;
                var reservationEndTime = y.Time.AddMinutes(y.Service!.Duration!.Value.Minutes);
                var newReservationStartTime = dto.Time;
                var newReservationEndTime = dto.Time.AddMinutes(selectedService!.Duration!.Value.Minutes);
                return newReservationStartTime <= reservationEndTime &&
                reservationStartTime <= newReservationEndTime;
            });

            if (isAvailable)
                return true;
            else
                return false;
        }
        public async Task<List<string>> GetReservationSuggestions(CreateReservationDto dto)
        {
            if (dto.BarberId != 0)
            {
                Barber? barber = await _barberRepository.GetBarberByIdWithServices(dto.BarberId);
                if (barber != null)
                {
                    var reservationList = await _reservationRepository.GetReservationsByBarberId(ReservationStatus.Pending, dto.BarberId);
                    TimeOnly jobStartTime = barber.StartTime; // Mesai başlangıç saati
                    TimeOnly jobEndTime = barber.EndTime; // Mesai bitiş saati
                    TimeSpan totalWorkTimeSpan = jobEndTime - jobStartTime; // Kaç saat çalışıyor bu berber?
                    TimeOnly currentHour = jobStartTime;

                    List<string> doluSaatler = new List<string>();
                    List<string> bulunanSaatler = new List<string>();

                    var services = await _serviceRepository.GetServicesByBarberId(dto.BarberId);
                    var selectedService = services.Where(x => x.Id == dto.ServiceId).FirstOrDefault(); // Seçilen hizmetin süresine erişmek için !

                    var reservations = reservationList.Where(x => x.Date == DateOnly.FromDateTime(DateTime.Today));

                    /*
                     * Eğer ki müşterinin istediği saat doluysa 10 adet öneri sunacak.
                     * Öneriler gelirken dinamik olarak gün atlıyor. (31.12 tarihinde boş rezervasyon yoksa 01.01 tarihine geçiyor ve boş rezervasyon bulana kadar devam ediyor)
                     */

                    int i = 0;
                    int foundReservation = 0;
                    while (foundReservation < 10)
                    {
                        currentHour = jobStartTime; // Değişecek olduğu için tekrar atandı

                        reservations = reservationList.Where(x => x.Date == DateOnly.FromDateTime(DateTime.Today.AddDays(i))); //
                        
                        var date = DateTime.Today.AddDays(i);

                        while (currentHour <= jobEndTime && foundReservation < 10) // Bakılan saat mesai saati içindeyse ve bulunan öneri 10 dan az ise.
                        {
                            if (currentHour >= jobEndTime) // Mesai saati dışına çıkılmışsa çık
                                break;

                            if (currentHour == jobStartTime)
                                currentHour = currentHour.AddMinutes(15); // Adam işe başladığı gibi müşteri almasın 15 dakika sonraya ilk randevu alınabilsin.

                            if (currentHour == jobEndTime)
                                currentHour = currentHour.AddMinutes(-30); // Adam mesaisi bittiği zaman randevu alamasın 30 dakika önceye son randevu alınabilsin

                            var isAvailable = !reservations.Any(y =>
                            {
                                var reservationStartTime = y.Time;
                                var reservationEndTime = y.Time.AddMinutes(y.Service!.Duration!.Value.Minutes);
                                var newReservationStartTime = currentHour;
                                var newReservationEndTime = currentHour.AddMinutes(selectedService!.Duration!.Value.Minutes);
                                return newReservationStartTime <= reservationEndTime &&
                                reservationStartTime <= newReservationEndTime;
                            });
                            if (isAvailable)
                            {
                                // ÖNERİ
                                bulunanSaatler.Add($"{date.ToString("dd/mm/yyyy")} - {currentHour.ToString()}");
                                foundReservation++;
                            }
                            else
                            {
                                // DOLU
                                doluSaatler.Add(currentHour.ToString());
                            }
                            currentHour = currentHour.AddMinutes(15);
                        }
                        i++;
                    }
                    return bulunanSaatler;
                }
            }
            return new List<string>();
        }
    }
}
