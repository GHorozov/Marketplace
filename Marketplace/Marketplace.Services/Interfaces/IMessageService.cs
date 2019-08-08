using Marketplace.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IMessageService
    {
        Task<bool> Create(string userId, string name, string email, string phone, string message);

        IQueryable<TModel> GetUserMessages<TModel>(string id);

        Task<bool> MarkAsReadMessageById(string userId, string id);

        Task<bool> DeleteMessageById(string userId, string id);

        Task<Message> GetMessageById(string id);
    }
}
