using AutoMapper;
using Marketplace.App.Areas.Administrator.ViewModels.Users;
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
    public class UserServiceTests
    {
        //GetAllUsers
        [Fact]
        public void GetAllUsersReturnsRightUsersModelCount()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetAllUsersReturnsRightUsersModelCount")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(dbContext, mapper, userManagerMock.Object);


            var user1 = new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84", FirstName = "Georgi", LastName = "Horozov", Email = "georgi@abv.bg" };
            var user2 = new MarketplaceUser() { Id = "3663f872-ca4b-4bb7-bfca-d56fb5de426e", FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@abv.bg" };
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.SaveChanges();

            //Act 
            var result = userService.GetAllUsers<UserViewModel>();
            var actual = result.Count();
            var expected = 2;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllUsersReturnsRightUserModelEmail()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetAllUsersReturnsRightUserModelEmail")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            var user1 = new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84", FirstName = "Georgi", LastName = "Horozov", Email = "georgi@abv.bg" };
            dbContext.Users.Add(user1);
            dbContext.SaveChanges();

            //Act 
            var result = userService.GetAllUsers<UserViewModel>();
            var actual = result.First().Email;
            var expected = "georgi@abv.bg";
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllUsersWithNoUsersReturnsCountZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetAllUsersWithNoUsersReturnsCountZero")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            //Act 
            var result = userService.GetAllUsers<UserViewModel>();
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        //DeleteById
        [Fact]
        public async Task DeleteByIdWithCorrectInputIdReturnsTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("DeleteByIdWithCorrectInputIdReturnsTrue")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var user = new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84", FirstName = "Georgi", LastName = "Horozov", Email = "georgi@abv.bg" };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.FindByIdAsync(user.Id))
                .Returns(Task.FromResult<MarketplaceUser>(user));

            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            var userId = "eb243391-a7ee-42f6-bb4e-f28c74a68d84";
            //Act 
            var actual = await userService.DeleteById(userId);
            var expected = true;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteByIdWithInCorrectInputReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("DeleteByIdWithInCorrectInputReturnsFalse")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var user = new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84", FirstName = "Georgi", LastName = "Horozov", Email = "georgi@abv.bg" };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.FindByIdAsync(user.Id))
                .Returns(Task.FromResult<MarketplaceUser>(null));

            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            var userId = "eb243391-a7ee-42f6-bb4e-f28c74a68d84";
            //Act 
            var actual = await userService.DeleteById(userId);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetUserById
        [Fact]
        public async Task GetUserByIdReturnsUserWithId()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetUserByIdReturnsUserWithId")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84" };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84";
            //Act 
            var actual = await userService.GetUserById(id);
            var expected = "eb243391-a7ee-42f6-bb4e-f28c74a68d84";
            //Assert
            Assert.Equal(expected, actual.Id);
        }

        [Fact]
        public async Task GetUserByIdWithIncorectidReturnsNull()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetUserByIdWithIncorectidReturnsNull")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84" };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var id = "";
            //Act 
            var actual = await userService.GetUserById(id);
            MarketplaceUser expected = null;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetUserRoles
        [Fact]
        public async Task GetUserRolesWithCorrectInputReturnsRole()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetUserRolesWithCorrectInputReturnsRole")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var usersList = new List<MarketplaceUser>()
            {
                  new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84", FirstName ="Ivan", Email="ivan@abv.bg" },
                  new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d83", FirstName = "Gosho", Email = "gosho@abv.bg" },
            };
            dbContext.Users.Add(usersList[0]);
            dbContext.Users.Add(usersList[1]);
            await dbContext.SaveChangesAsync();

            var roleName = "user";
            var listRoles = new List<string>() { "user", "admin" };

            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.GetUsersInRoleAsync(roleName))
                    .Returns(Task.FromResult<IList<MarketplaceUser>>(usersList.ToList()));

            userManagerMock.Setup(x => x.GetRolesAsync(usersList[0]))
                    .Returns(Task.FromResult<IList<string>>(listRoles));

            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            var id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84";
            //Act 
            var result = await userService.GetUserRoles(id);
            var actual = result.First();
            var expected = "user";
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetUserRolesWithInCorrectIdThrowsNull()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetUserRolesWithInCorrectIdThrowsNull")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var usersList = new List<MarketplaceUser>()
            {
                  new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84", FirstName ="Ivan", Email="ivan@abv.bg" },
                  new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d83", FirstName = "Gosho", Email = "gosho@abv.bg" },
            };
            dbContext.Users.Add(usersList[0]);
            dbContext.Users.Add(usersList[1]);
            await dbContext.SaveChangesAsync();

            var roleName = "user";
            var listRoles = new List<string>() { "user", "admin" };

            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.GetUsersInRoleAsync(roleName))
                    .Returns(Task.FromResult<IList<MarketplaceUser>>(usersList.ToList()));

            userManagerMock.Setup(x => x.GetRolesAsync(usersList[0]))
                    .Returns(Task.FromResult<IList<string>>(listRoles));

            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            var id = "";
            //Act 
            var result = await userService.GetUserRoles(id);
            var actual = result.ToList();
            var expected = new List<string>();
            //Assert
            Assert.Equal(expected, actual);
        }

        //ChangePassword

        //GetAllWishProductsCount
        [Fact]
        public async Task GetAllWishProductsCountReturnsOne()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetAllWishProductsCountReturnsOne")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84" };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var product = new Product() { Id = "6093d234-12b9-4b9b-9391-97475e24d7b3" };
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            var wishProduct = new WishProduct() { MarketplaceUserId = user.Id, ProductId = product.Id };
            dbContext.WishProducts.Add(wishProduct);
            await dbContext.SaveChangesAsync();

            //Act 
            var actual = userService.GetAllWishProductsCount(user);
            var expected = 1;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAllWishProductsCountReturnsZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetAllWishProductsCountReturnsZero")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var userStoreMock = new Mock<IUserStore<MarketplaceUser>>();
            var userManagerMock = new Mock<UserManager<MarketplaceUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(dbContext, mapper, userManagerMock.Object);

            var user = new MarketplaceUser() { Id = "eb243391-a7ee-42f6-bb4e-f28c74a68d84" };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            //Act 
            var actual = userService.GetAllWishProductsCount(user);
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
