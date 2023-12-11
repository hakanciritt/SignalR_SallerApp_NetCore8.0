using SignalR_App.Application.Dtos.CategoryDto;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<Result> Create(CategoryDto booking);
        Task<Result> Delete(int id);
        Task<Result> Update(CategoryDto booking);
        Task<DataResult<CategoryDto>> GetById(int id);
    }
}
