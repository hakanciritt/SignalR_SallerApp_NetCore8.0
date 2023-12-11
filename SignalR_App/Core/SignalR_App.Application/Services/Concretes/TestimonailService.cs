using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.TestimonialDtos;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Concretes
{
    public class TestimonailService(IRepository<Testimonial, int> testimonialRepository) : ITestimonailService
    {
        private readonly IRepository<Testimonial, int> _testimonialRepository = testimonialRepository;

        public async Task<List<TestimonialDto>> GetAll()
        {
            var result = await _testimonialRepository.GetAll()
                .Where(c => c.Status == Domain.Enums.Status.Active).ToListAsync();
            return ObjectMapper.Map.Map<List<TestimonialDto>>(result);
        }
        public async Task<DataResult<TestimonialDto>> GetById(int id)
        {
            var result = await _testimonialRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);
            if (result is null) return DataResult<TestimonialDto>.Failed("Yorum bulunamadı");

            return DataResult<TestimonialDto>.Successed(ObjectMapper.Map.Map<TestimonialDto>(result));
        }
        public async Task<Result> Create(TestimonialDto testimonial)
        {
            var mapping = ObjectMapper.Map.Map<Testimonial>(testimonial);
            var result = await _testimonialRepository.InsertAsync(mapping);
            await _testimonialRepository.SaveChangesAsync();
            return result != null ? Result.Successed() : Result.Failed();
        }
        public async Task<Result> Delete(int id)
        {
            await _testimonialRepository.DeleteAsync(id);
            await _testimonialRepository.SaveChangesAsync();
            return Result.Successed("Başarıyla silindi");
        }
        public async Task<Result> Update(TestimonialDto testimonial)
        {
            var result = await _testimonialRepository.GetAll().FirstOrDefaultAsync(c => c.Id == testimonial.Id);
            ObjectMapper.Map.Map(testimonial, result);
            await _testimonialRepository.SaveChangesAsync();

            return Result.Successed("Başarıyla güncellendi");
        }
    }
}
