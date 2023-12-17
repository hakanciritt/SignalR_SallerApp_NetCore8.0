using SignalR_App.Application.Dtos.TextContentDtos;
using SignalR_App.Domain.Result;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface ITextContentService
    {
        Task<List<TextContentDto>> GetAll();
        Task<TextContentDto> GetTextContentByKey(string key);
        Task<TextContentDto> GetById(int key);
        Task<Result> Delete(int id);
        Task<Result> Update(TextContentDto textContent);
        Task<Result> Create(TextContentDto textContent);
    }
}
