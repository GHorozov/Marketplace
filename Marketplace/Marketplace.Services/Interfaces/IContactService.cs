using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IContactService
    {
        Task<bool> Create(string name, string email, string phone, string message); 
    }
}
