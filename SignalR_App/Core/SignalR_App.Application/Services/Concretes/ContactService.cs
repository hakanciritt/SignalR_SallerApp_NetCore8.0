using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.ContactDtos;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Concretes
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact, int> _contactRepository;

        public ContactService(IRepository<Contact, int> contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<List<ContactDto>> GetAll()
        {
            var result = await _contactRepository.GetAll().ToListAsync();
            return ObjectMapper.Map.Map<List<ContactDto>>(result);
        }
        public async Task<DataResult<ContactDto>> GetById(int id)
        {
            var result = await _contactRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);
            if (result is null) return DataResult<ContactDto>.Failed("İletişim bilgisi bulunamadı");
            if (!result.IsRead)
            {
                result.IsRead = true;
                await _contactRepository.SaveChangesAsync();
            }

            return DataResult<ContactDto>.Successed(ObjectMapper.Map.Map<ContactDto>(result));
        }
        public async Task<Result> Create(ContactDto booking)
        {
            var mapping = ObjectMapper.Map.Map<Contact>(booking);
            mapping.IsRead = false;
            var result = await _contactRepository.InsertAsync(mapping);
            await _contactRepository.SaveChangesAsync();

            return result != null ? Result.Successed() : Result.Failed();
        }
        public async Task<Result> Delete(int id)
        {
            await _contactRepository.DeleteAsync(id);
            await _contactRepository.SaveChangesAsync();
            return Result.Successed("Başarıyla silindi");
        }
    }
}
