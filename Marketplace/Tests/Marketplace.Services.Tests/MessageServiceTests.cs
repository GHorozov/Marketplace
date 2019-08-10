using AutoMapper;
using Marketplace.App.Areas.Administrator.ViewModels.Messages;
using Marketplace.App.AutoMapperConfigurations;
using Marketplace.Data;
using Marketplace.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Services.Tests
{
    public class MessageServiceTests
    {
        //Create
        [Fact]
        public async Task CreateWithCorrectInputreturnTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("CreateWithCorrectInputreturnTrue")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311111", Email = "ivan@abv.bg" };
            var listUsers = new List<MarketplaceUser>();
            listUsers.Add(user);
            listUsers.Add(user1);
            dbContext.Users.Add(user);
            dbContext.Users.Add(user1);
            await dbContext.SaveChangesAsync();

            var role = "Administrator";

            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.GetUsersInRoleAsync(role))
                .Returns(Task.FromResult<IList<MarketplaceUser>>(listUsers.ToList()));

            userManagerMock.Setup(x => x.IsInRoleAsync(user, role))
               .Returns(Task.FromResult(true));

            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var userId = "cd89dcbc-bb80-4246-b9b8-ad2175311111";
            var name = "Ivan";
            var email = "ivan@abv.bg";
            var phone = "0883477980";
            var message = "This is test message!";

            //Act 
            var actual = await messageService.Create(userId, name, email, phone, message);
            var expected = true;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateWithInCorrectInputIdReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("CreateWithInCorrectInputIdReturnFalse")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311111", Email = "ivan@abv.bg" };
            var listUsers = new List<MarketplaceUser>();
            listUsers.Add(user);
            listUsers.Add(user1);
            dbContext.Users.Add(user);
            dbContext.Users.Add(user1);
            await dbContext.SaveChangesAsync();

            var role = "Administrator";

            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.GetUsersInRoleAsync(role))
                .Returns(Task.FromResult<IList<MarketplaceUser>>(listUsers.ToList()));

            userManagerMock.Setup(x => x.IsInRoleAsync(user, role))
               .Returns(Task.FromResult(true));

            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var userId = "";
            var name = "Ivan";
            var email = "ivan@abv.bg";
            var phone = "0883477980";
            var message = "This is test message!";

            //Act 
            var actual = await messageService.Create(userId, name, email, phone, message);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetUserMessages
        [Fact]
        public async Task GetUserMessagesCorrectIdReturnMessage()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("GetUserMessagesCorrectIdReturnMessage")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var userId = "cd89dcbc-bb80-4246-b9b8-ad21753111cc";
            //Act 
            var result = messageService.GetUserMessages<AdminMessageViewModel>(userId);
            var actual = result.First().Email;
            var expected = "ivan@abv.bg";
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetUserMessagesCorrectIdMarkAsReadSetToTrueReturnZeroMessages()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("GetUserMessagesCorrectIdMarkAsReadSetToTrueReturnZeroMessages")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = true,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var userId = "cd89dcbc-bb80-4246-b9b8-ad21753111cc";
            //Act 
            var result = messageService.GetUserMessages<AdminMessageViewModel>(userId);
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetUserMessagesWithInCorrectIdReturnCountZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("GetUserMessagesWithInCorrectIdReturnCountZero")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var userId = "";
            //Act 
            var result = messageService.GetUserMessages<AdminMessageViewModel>(userId);
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        //MarkAsReadMessageById
        [Fact]
        public async Task MarkAsReadMessageByIdCorrectInputReturnsTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("MarkAsReadMessageByIdCorrectInputReturnsTrue")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var userId = "cd89dcbc-bb80-4246-b9b8-ad21753111cc";
            var messageId = message.Id;

            //Act 
            var actual = await messageService.MarkAsReadMessageById(userId, messageId);
            var expected = true;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task MarkAsReadMessageByIdInCorrectUserIdInputReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("MarkAsReadMessageByIdInCorrectUserIdInputReturnsFalse")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var userId = "cd89dcbc-bb80-4246-b9b8-ad21753111ee"; //incorect id
            var messageId = message.Id;

            //Act 
            var actual = await messageService.MarkAsReadMessageById(userId, messageId);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task MarkAsReadMessageByIdInCorrectMessageIdInputReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("MarkAsReadMessageByIdInCorrectMessageIdInputReturnsFalse")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var userId = "cd89dcbc-bb80-4246-b9b8-ad21753111cc";
            var messageId = "";

            //Act 
            var actual = await messageService.MarkAsReadMessageById(userId, messageId);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        //DeleteMessageById
        [Fact]
        public async Task DeleteMessageByIdCorrectInputReturnTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("DeleteMessageByIdCorrectInputReturnTrue")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var userId = "cd89dcbc-bb80-4246-b9b8-ad21753111cc";
            var messageId = message.Id;

            //Act 
            var actual = await messageService.DeleteMessageById(userId, messageId);
            var expected = true;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteMessageByIdWithInCorrectUserIdInputReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("DeleteMessageByIdWithInCorrectUserIdInputReturnFalse")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var userId = "cd89dcbc-bb80-4246-b9b8-ad2175311111"; //incorect userId
            var messageId = message.Id;

            //Act 
            var actual = await messageService.DeleteMessageById(userId, messageId);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteMessageByIdWithInCorrectMessageIdInputReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("DeleteMessageByIdWithInCorrectMessageIdInputReturnFalse")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var userId = "cd89dcbc-bb80-4246-b9b8-ad21753111cc";
            var messageId = "1"; //incorect messageId

            //Act 
            var actual = await messageService.DeleteMessageById(userId, messageId);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetMessageById
        [Fact]
        public async Task GetMessageByIdWithCorrectIdReturnsCorrectMessageEmail()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("GetMessageByIdWithCorrectIdReturnsCorrectMessageEmail")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var messageId = message.Id;
            //Act 
            var result = await messageService.GetMessageById(messageId);
            var actual = result.Email;
            var expected = "ivan@abv.bg";
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetMessageByIdWithInCorrectIdReturnsNull()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("GetMessageByIdWithInCorrectIdReturnsNull")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var messageService = new MessageService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad21753111cc", Email = "georgi@abv.bg" };
            var user1 = new MarketplaceUser() { Id = "cd89dcbc-bb80-4246-b9b8-ad2175311123", Email = "ivan@abv.bg" };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var message = new Message()
            {
                MarketplaceUserId = user1.Id,
                Phone = "0883377905",
                Name = "Ivan",
                IssuedOn = DateTime.UtcNow,
                MessageContent = "This is test message!",
                Email = "ivan@abv.bg",
                MarkAsRead = false,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            user.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            var messageId = "1";
            //Act 
            var actual = await messageService.GetMessageById(messageId); ;
            Message expected = null;
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
