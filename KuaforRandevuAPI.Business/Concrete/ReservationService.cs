using AutoMapper;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Common.Responses;
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
        public async Task<ApiResponse<List<ResultReservationDto>>> GetAllReservations()
        {
            var reservations = await _repository.GetAll();
            var data = _mapper.Map<List<ResultReservationDto>>(reservations);
            return ApiResponse<List<ResultReservationDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<ResultReservationDto>> GetReservationById(int id)
        {
            var reservation = await _repository.GetById(id);
            var data = _mapper.Map<ResultReservationDto>(reservation);
            return ApiResponse<ResultReservationDto>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultReservationDto>>> GetReservationForToday()
        {
            var reservations = await _reservationRepository.GetReservationsForToday();
            var data = _mapper.Map<List<ResultReservationDto>>(reservations);
            return ApiResponse<List<ResultReservationDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultReservationDto>>> GetReservationByBarberId(ReservationStatus status, int barberId)
        {
            var reservations = await _reservationRepository.GetReservationsByBarberId(status, barberId);
            var data = _mapper.Map<List<ResultReservationDto>>(reservations);
            return ApiResponse<List<ResultReservationDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<CreateReservationDto>> Create(CreateReservationDto dto)
        {
            bool hourAvailable = await CheckHourAvailable(_mapper.Map<ResultReservationDto>(dto)); // Mesai saatleri kontrolü
            if (hourAvailable)
            {
                // Mesai saatleri içinde.
                var reservationAvailable = await CheckReservationAvailable(_mapper.Map<ResultReservationDto>(dto));

                if (reservationAvailable)
                {
                    // Rezervasyon şartları uygun.
                    await _repository.Add(_mapper.Map<Reservation>(dto));
                    return ApiResponse<CreateReservationDto>.SuccessResponse(dto, "OK");
                }
                else
                {
                    // Rezervasyon saati dolu, önerileri getir.
                    var suggestions = await GetReservationSuggestions(_mapper.Map<ResultReservationDto>(dto));
                    return ApiResponse<CreateReservationDto>.ErrorResponse("Rezervasyon Saati Dolu, 10 Adet Öneri", suggestions, 400);
                }
            }
            else
            {
                // Mesai saatleri dışında!
                return ApiResponse<CreateReservationDto>.ErrorResponse("Mesai Saatleri Dışında!");
            }
        }
        public async Task<ApiResponse<UpdateReservationDto>> Update(UpdateReservationDto dto)
        {
            var updatedReservation = await _repository.GetById(dto.Id);
            if(updatedReservation != null)
            {
                bool hourAvailable = await CheckHourAvailable(_mapper.Map<ResultReservationDto>(dto)); // Mesai saatleri kontrolü
                if (hourAvailable)
                {
                    // Mesai saatleri içinde.
                    var reservationAvailable = await CheckReservationAvailable(_mapper.Map<ResultReservationDto>(dto));

                    if (reservationAvailable)
                    {
                        // Rezervasyon şartları uygun.
                        updatedReservation.Name = dto.Name;
                        updatedReservation.Status = dto.Status;
                        updatedReservation.PhoneNumber = dto.PhoneNumber;
                        updatedReservation.BarberId = dto.BarberId;
                        updatedReservation.Date = dto.Date;
                        await _repository.Update(updatedReservation);
                        return ApiResponse<UpdateReservationDto>.SuccessResponse(dto, "OK");
                    }
                    else
                    {
                        // Rezervasyon saati dolu, önerileri getir.
                        var suggestions = await GetReservationSuggestions(_mapper.Map<ResultReservationDto>(dto));
                        return ApiResponse<UpdateReservationDto>.ErrorResponse("Rezervasyon Saati Dolu, 10 Adet Öneri", suggestions, 400);
                    }
                }
                else
                {
                    return ApiResponse<UpdateReservationDto>.ErrorResponse("Mesai Saatleri Dışında!");
                }
            }
            else
            {
                // Güncellenecek Rezervasyon NULL!
                return ApiResponse<UpdateReservationDto>.ErrorResponse("Not Found", null, 404);
            }
        }
        public async Task<ApiResponse<int>> Remove(int id)
        {
            var service = await _repository.GetById(id);
            if (service != null)
            {
                await _repository.Remove(service);
                return ApiResponse<int>.SuccessResponse(id, "OK");
            }
            else
            {
                return ApiResponse<int>.ErrorResponse("Not Found", null, 404);
            }
        }
        public async Task<bool> CheckHourAvailable(ResultReservationDto dto)
        {

            var barber = await _barberRepository.GetBarberByIdWithServices(dto.BarberId);

            if (barber != null)
            {
                // Saat Kontrolü
                if (barber.StartTime < dto.Time && barber.EndTime > dto.Time)
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

            return false;
        }
        public async Task<bool> CheckReservationAvailable(ResultReservationDto dto)
        {
            var reservations = await _reservationRepository.GetReservationsByBarberId(ReservationStatus.Pending,dto.BarberId);
            var services = await _serviceRepository.GetServicesByBarberId(dto.BarberId);
            var selectedService = services.Where(x => x.Id == dto.ServiceId).FirstOrDefault(); // Seçilen hizmetin süresine erişmek için !

            var reservationsForDate = reservations.Where(x => x.Date == dto.Date);

            if(dto.Id != 0)
            {
                // Id 0 değilse güncelleme işlemidir.
                return true;
            }

            var isAvailable = !reservationsForDate.Any(y =>
            {
                var reservationStartTime = y.Time;
                var reservationEndTime = y.Time.AddMinutes(y.Service!.Duration!.Value.Minutes);
                var newReservationStartTime = dto.Time;
                var newReservationEndTime = dto.Time.AddMinutes(selectedService!.Duration!.Value.Minutes);
                return newReservationStartTime < reservationEndTime &&
                reservationStartTime < newReservationEndTime;
            });

            if (isAvailable)
                return true;
            else
                return false;
        }
        public async Task<List<string>> GetReservationSuggestions(ResultReservationDto dto)
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

                                var reservationId = y.Id;

                                //      10:00                       09:45
                                //      09:00                       10:45
                                return newReservationStartTime < reservationEndTime &&
                                reservationStartTime < newReservationEndTime;
                            });
                            if (isAvailable)
                            {
                                // ÖNERİ
                                bulunanSaatler.Add($"{date.ToString("dd/MM/yyyy")} - {currentHour.ToString()}");
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
