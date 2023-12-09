using SignalR_App.Application.Dtos.ProductDtos;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();
        Task<Result> Create(ProductDto product);
        Task<Result> Delete(int id);
        Task<Result> Update(ProductDto product);
        Task<DataResult<ProductDto>> GetById(int id);
    }
}
