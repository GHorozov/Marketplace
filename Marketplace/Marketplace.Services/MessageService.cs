using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class MessageService : IMessageService
    {
        private const string AdministratorRole = "Administrator";
        private readonly MarketplaceDbContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly UserManager<MarketplaceUser> userManager;

        public MessageService(MarketplaceDbContext context,IMapper mapper, IUserService userService, UserManager<MarketplaceUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<bool> Create(string userId, string name, string email, string phone, string messageContent)
        {
            var message = new Message()
            {
                Name = name,
                Email = email,
                Phone = phone,
                MessageContent = messageContent,
                IssuedOn = DateTime.UtcNow,
                MarketplaceUserId = userId,
            };

            this.context.Messages.Add(message);
            var isMeassageSaved = await this.context.SaveChangesAsync();
            if (isMeassageSaved <= 0) return false;

            var allUsers = this.context.Users.ToList();
            foreach (var currentUser in allUsers)
            {
                if(await this.userManager.IsInRoleAsync(currentUser, AdministratorRole))
                {
                    currentUser.Messages.Add(message);
                }
            }

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<TModel> GetUserMessages<TModel>(string id)
        {
            var result = this.context
                .Messages
                .Where(x => x.MarketplaceUserId == id && x.MarkAsRead == false)
                .ProjectTo<TModel>(mapper.ConfigurationProvider);

            return result;
        }

        public async Task<bool> MarkAsReadMessageById(string userId, string id)
        {
            var targetMessage = await this.context
                .Messages
                .Where(x => x.MarketplaceUserId == userId && x.Id == id)
                .SingleOrDefaultAsync();

            if (targetMessage == null) return false;

            targetMessage.MarkAsRead = true;
            this.context.Messages.Update(targetMessage);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
        public async Task<bool> DeleteMessageById(string userId, string id)
        {
            var targetMessage = await this.context
                .Messages
                .Where(x => x.MarketplaceUserId == userId && x.Id == id)
                .SingleOrDefaultAsync();

            if (targetMessage == null) return false;

            this.context.Messages.Remove(targetMessage);

            var user = await this.userService.GetUserById(userId);
            user.Messages.Remove(targetMessage);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Message> GetMessageById(string id)
        {
            var message = await this.context
                .Messages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return message;
        }
    }
}
