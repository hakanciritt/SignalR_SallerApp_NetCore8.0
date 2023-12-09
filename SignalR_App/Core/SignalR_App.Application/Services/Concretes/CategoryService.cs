using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.CategoryDto;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Concretes
{
    public class CategoryService(IRepository<Category, int> categoryRepository) : ICategoryService
    {
        private readonly IRepository<Category, int> _categoryRepository = categoryRepository;

        public async Task<List<CategoryDto>> GetAll()
        {
            var result = await _categoryRepository.GetAll()
                .Where(c => c.Status == Domain.Enums.Status.Active).ToListAsync();
            return ObjectMapper.Map.Map<List<CategoryDto>>(result);
        }
        public async Task<DataResult<CategoryDto>> GetById(int id)
        {
            var result = await _categoryRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);
            if (result is null) return DataResult<CategoryDto>.Failed("Booking bulunamadı");

            return DataResult<CategoryDto>.Successed(ObjectMapper.Map.Map<CategoryDto>(result));
        }

        public async Task<Result> Create(CategoryDto booking)
        {
            var mapping = ObjectMapper.Map.Map<Category>(booking);
            var result = await _categoryRepository.InsertAsync(mapping);
            await _categoryRepository.SaveChangesAsync();
            return result != null ? Result.Successed() : Result.Failed();
        }
        public async Task<Result> Delete(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            await _categoryRepository.SaveChangesAsync();
            return Result.Successed("Başarıyla silindi");
        }
        public async Task<Result> Update(CategoryDto booking)
        {
            var result = await _categoryRepository.GetAll().FirstOrDefaultAsync(c => c.Id == booking.Id);
            ObjectMapper.Map.Map(booking, result);
            await _categoryRepository.SaveChangesAsync();

            return Result.Successed("Başarıyla güncellendi");
        }
    }
}
