using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.TextContentDtos;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Result;

namespace SignalR_App.Application.Services.Concretes
{
    public class TextContentService : ITextContentService
    {
        private readonly IRepository<TextContent, int> _textContentRepository;

        public TextContentService(IRepository<TextContent, int> textContentRepository)
        {
            _textContentRepository = textContentRepository;
        }
        public async Task<TextContentDto> GetTextContentByKey(string key)
        {
            var result = await _textContentRepository.GetAll()
                .Include(d => d.Meta).FirstOrDefaultAsync(c => c.Key == key);
            return ObjectMapper.Map.Map<TextContentDto>(result);
        }
        public async Task<Result> Delete(int id)
        {
            await _textContentRepository.DeleteAsync(id);
            await _textContentRepository.SaveChangesAsync();

            return Result.Successed();
        }

        public async Task<Result> Update(TextContentDto textContent)
        {
            var data = await _textContentRepository.GetAll().Include(d => d.Meta)
                .FirstOrDefaultAsync(c => c.Id == textContent.Id);
            ObjectMapper.Map.Map(textContent, data);
            await _textContentRepository.SaveChangesAsync();

            return Result.Successed();
        }
        public async Task<Result> Create(TextContentDto textContent)
        {
            var mapping = ObjectMapper.Map.Map<TextContent>(textContent);

            var data = await _textContentRepository.InsertAsync(mapping);
            await _textContentRepository.SaveChangesAsync();
            return data != null ? Result.Successed() : Result.Failed("Bir hata meydana geldi");
        }
    }
}
