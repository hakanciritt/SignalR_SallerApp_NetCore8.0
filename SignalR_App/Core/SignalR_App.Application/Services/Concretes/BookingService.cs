using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.BookingDto;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Concretes
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking, int> _bookingRepository;
        public BookingService(IRepository<Booking, int> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<List<BookingDto>> GetAll()
        {
            var result = await _bookingRepository.GetAll().ToListAsync();
            return ObjectMapper.Map.Map<List<BookingDto>>(result);
        }
        public async Task<DataResult<BookingDto>> GetById(int id)
        {
            var result = await _bookingRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);
            if (result is null) return DataResult<BookingDto>.Failed("Booking bulunamadı");

            return DataResult<BookingDto>.Successed(ObjectMapper.Map.Map<BookingDto>(result));
        }
        public async Task<Result> Create(BookingDto booking)
        {
            var mapping = ObjectMapper.Map.Map<Booking>(booking);
            var result = await _bookingRepository.InsertAsync(mapping);
            await _bookingRepository.SaveChangesAsync();
            return result != null ? Result.Successed() : Result.Failed();
        }
        public async Task<Result> Delete(int id)
        {
            await _bookingRepository.DeleteAsync(id);
            await _bookingRepository.SaveChangesAsync();
            return Result.Successed("Başarıyla silindi");
        }
        public async Task<Result> Update(BookingDto booking)
        {
            var result = await _bookingRepository.GetAll().FirstOrDefaultAsync(c => c.Id == booking.Id);
            ObjectMapper.Map.Map(booking, result);
            await _bookingRepository.SaveChangesAsync();

            return Result.Successed("Başarıyla güncellendi");
        }
    }
}
