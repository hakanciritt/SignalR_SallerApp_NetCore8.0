using SignalR_App.Application.Dtos.TestimonialDtos;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface ITestimonailService
    {
        Task<List<TestimonialDto>> GetAll();
        Task<Result> Create(TestimonialDto booking);
        Task<Result> Delete(int id);
        Task<Result> Update(TestimonialDto booking);
        Task<DataResult<TestimonialDto>> GetById(int id);
    }
}
