using Marketplace.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class ContactService : IContactService
    {
        public Task<bool> Create(string name, string email, string phone, string message)
        {
            throw new NotImplementedException();
        }
    }
}
