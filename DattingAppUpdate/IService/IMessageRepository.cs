using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DattingAppUpdate.IService
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
        Task<bool> SaveAllAsync();
    }
}
