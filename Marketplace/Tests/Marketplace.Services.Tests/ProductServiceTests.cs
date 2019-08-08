using AutoMapper;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Services.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task AddProductShouldReturnTrue()
        {
            //Arrange
            var dbContext = GetDbContext();
            var autoMapperMock = new Mock<IMapper>();
            var productService = new ProductService(dbContext, autoMapperMock.Object);
            var product = new Product() { Name = "Smartphone", Price = 450, Quantity = 1 };
            //Act 
            var actual = await productService.AddProduct(product);
            var expected = true;
            //Assert
            Assert.True(actual.Equals(expected));
        }

        

        [Fact]
        public async Task AddProductShouldReturnFalse()
        {
            //Arrange
            var dbContext = GetDbContext();
            var autoMapperMock = new Mock<IMapper>();
            var productService = new ProductService(dbContext, autoMapperMock.Object);
            Product product = null;
            //Act 
            var actual = await productService.AddProduct(product);
            var expected = false;
            //Assert
            Assert.True(actual.Equals(expected));
        }

        [Fact]
        public async Task AddProductCheckCountWithInMemoryDatabase()
        {
            //Arrange
            var dbContext = GetDbContext();
            var autoMapperMock = new Mock<IMapper>();
            var productService = new ProductService(dbContext, autoMapperMock.Object);
            var product = new Product() { Name = "Smartphone", Price = 450, Quantity = 1 };
            //Act 
            await productService.AddProduct(product);
            var actual = dbContext.Products.ToArray().Length;
            var expected = 1;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddProductCheckQuantityWithInMemoryDatabase()
        {
            //Arrange
            var dbContext = GetDbContext();
            var autoMapperMock = new Mock<IMapper>();
            var productService = new ProductService(dbContext, autoMapperMock.Object);
            var product = new Product() { Name = "Smartphone", Price = 450, Quantity = 1 };
            //Act 
            await productService.AddProduct(product);
            var actual = dbContext.Products.First().Quantity;
            var expected = product.Quantity;
            //Assert
            Assert.Equal(expected, actual);
        }





        private static MarketplaceDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                            .UseInMemoryDatabase("Tast-ProductService")
                            .Options;
            var dbContext = new MarketplaceDbContext(options);
            return dbContext;
        }
    }
}
