using AutoMapper;
using Marketplace.App.AutoMapperConfigurations;
using Marketplace.Data;
using Marketplace.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Services.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateWithCorrectInputOrderShouldReturnTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("AddProductShouldReturnTrue")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var orderService = new OrderService(dbContext, mapper);

            var user = new MarketplaceUser()
            {
                Id = "8ca6c061-52de-4f0a-8885-a7501b6dae79",
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "ivanivanov@abv.bg"
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            var cart = new ShoppingCart()
            {
                Id = "86f81ed7-312e-4760-a594-1c40389440bd",
                UserId = user.Id
            };
            dbContext.ShoppingCarts.Add(cart);
            await dbContext.SaveChangesAsync();
            var product = new Product()
            {
                Id = "532b377e-83f1-43db-a697-1e623107ae60",
                Name = "TestProduct",
                Price = 5.45m,
                Quantity = 1
            };
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            var shoppingCartProduct = new ShoppingCartProduct()
            {
                ProductId = product.Id,
                Quantity = product.Quantity,
                ShoppingCartId = cart.Id
            };
            cart.Products.Add(shoppingCartProduct);
            dbContext.ShoppingCarts.Update(cart);
            await dbContext.SaveChangesAsync();

            var phone = "0883288905";
            var shippingAddress = "ul.Stara Planina 23";
            //Act 
            var actual = await orderService.Create(user, phone, shippingAddress);
            var expected = true;
            //Assert
            Assert.True(actual.Equals(expected));
        }

        [Fact]
        public async Task CreateWithInputUserEqualTonullOrderShouldReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("CreateWithInputUserEqualTonullOrderShouldThrollExeption")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var orderService = new OrderService(dbContext, mapper);

            MarketplaceUser user = null;
            var phone = "0883288905";
            var shippingAddress = "ul.Stara Planina 23";
            //Act 
            var actual = await orderService.Create(user, phone, shippingAddress);
            var expected = false;
            //Assert
            Assert.True(actual.Equals(expected));
        }

        [Fact]
        public async Task CreateIfUserDoesNotHasCartThrowExeption()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("CreateIfUserDoesNotHasCartThrowExeption")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var orderService = new OrderService(dbContext, mapper);

            var user = new MarketplaceUser()
            {
                Id = "8ca6c061-52de-4f0a-8885-a7501b6dae79",
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "ivanivanov@abv.bg"
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var phone = "0883288905";
            var shippingAddress = "ul.Stara Planina 23";
            //Act 
            //Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => orderService.Create(user, phone, shippingAddress));
        }

        //GetAllOrders
        //[Fact]
        //public void GetAllOrdersShouldReturnOneModel()
        //{
        //    //Arrange
        //    var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
        //                .UseInMemoryDatabase("GetAllOrdersShouldReturnOneModel")
        //                .Options;
        //    var dbContext = new MarketplaceDbContext(options);
        //    var profile = new MarketplaceProfile();
        //    var configuration = new MapperConfiguration(x => x.AddProfile(profile));
        //    var mapper = new Mapper(configuration);
        //    var orderService = new OrderService(dbContext, mapper);

        //    var user = new MarketplaceUser()
        //    {
        //        Id = "8ca6c061-52de-4f0a-8885-a7501b6dae79",
        //        FirstName = "Ivan",
        //        LastName = "Ivanov",
        //        Email = "ivanivanov@abv.bg"
        //    };
        //    dbContext.Users.Add(user);
        //    dbContext.SaveChanges();

        //    var product = new Product()
        //    {
        //        Id = "532b377e-83f1-43db-a697-1e623107ae60",
        //        Name = "TestProduct",
        //        Price = 5.45m,
        //        Quantity = 1
        //    };
        //    dbContext.Products.Add(product);
        //    dbContext.SaveChanges();

        //    var order = new Order()
        //    {
        //        Id = "492a1470-1a88-40fd-bc09-358077449545",
        //        IssuedOn = DateTime.UtcNow,
        //        MarketplaceUserId = user.Id,
        //        Quantity = 1,
        //        Phone = "0884488905",
        //        ShippingAddress = "ul.Stara Planina 23",
        //    };

        //    var productOrder = new ProductOrder()
        //    {
        //         ProductId = product.Id,
        //          OrderId = order.Id
        //    };

        //    dbContext.ProductOrder.Add(productOrder);
        //    dbContext.SaveChanges();

        //    order.Products.Add(productOrder);
        //    dbContext.SaveChanges();

        //    //To do: To configure in profile model and run test

        //    //Act 
        //    var result = orderService.GetAllOrders<OrderViewModel>();
        //    var actual = result.Count();
        //    var expected = 1;
        //    //Assert
        //    Assert.Equal(expected, actual);
        //}
    }
}
