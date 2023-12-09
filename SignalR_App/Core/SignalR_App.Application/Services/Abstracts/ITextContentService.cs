using SignalR_App.Application.Dtos.TextContentDtos;
using SignalR_App.Domain.Result;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface ITextContentService
    {
        Task<TextContentDto> GetTextContentByKey(string key);
        Task<Result> Delete(int id);
    }
}
