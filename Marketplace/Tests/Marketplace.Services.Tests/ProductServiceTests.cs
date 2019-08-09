using AutoMapper;
using Marketplace.App.AutoMapperConfigurations;
using Marketplace.App.ViewModels.Categories;
using Marketplace.App.ViewModels.Home;
using Marketplace.App.ViewModels.Products;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Services.Tests
{
    public class ProductServiceTests
    {
        //AddProduct
        [Fact]
        public async Task AddProductShouldReturnTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("AddProductShouldReturnTrue")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

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
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                           .UseInMemoryDatabase("AddProductShouldReturnFalse")
                           .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

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
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                           .UseInMemoryDatabase("AddProductCheckCountWithInMemoryDatabase")
                           .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

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
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                          .UseInMemoryDatabase("AddProductCheckQuantityWithInMemoryDatabase")
                          .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Name = "Smartphone", Price = 450, Quantity = 1 };
            //Act 
            await productService.AddProduct(product);
            var actual = dbContext.Products.First().Quantity;
            var expected = product.Quantity;
            //Assert
            Assert.Equal(expected, actual);
        }

        //EditPicturePath
        [Fact]
        public async Task EditPicturePathShouldReturnTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                          .UseInMemoryDatabase("EditPicturePathShouldReturnTrue")
                          .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87" };
            dbContext.Products.Add(product);
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_1.jpg" };
            dbContext.Pictures.Add(picture);
            product.Pictures.Add(picture);
            dbContext.SaveChanges();
            var url = "/images/users/2019-view_6.jpg";
            ////Act 
            var actual = await productService.EditPicturePath(product.Id, url);
            var expected = true;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EditPicturePathEmptyProductIdShouldReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                          .UseInMemoryDatabase("EditPicturePathEmptyProductIdShouldReturnFalse")
                          .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87" };
            dbContext.Products.Add(product);
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_1.jpg" };
            dbContext.Pictures.Add(picture);
            product.Pictures.Add(picture);
            dbContext.SaveChanges();
            var productId = "";
            var url = "/images/users/2019-view_6.jpg";
            //Act 
            var actual = await productService.EditPicturePath(productId, url);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EditPicturePathProductIdIsNullShouldReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("EditPicturePathProductIdIsNullShouldReturnFalse")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87" };
            dbContext.Products.Add(product);
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_1.jpg" };
            dbContext.Pictures.Add(picture);
            product.Pictures.Add(picture);
            dbContext.SaveChanges();
            var productId = "Some test string";
            var url = "/images/users/2019-view_6.jpg";
            //Act 
            var actual = await productService.EditPicturePath(productId, url);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EditPicturePathNoPicturesAddedShouldReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("EditPicturePathNoPicturesAddedShouldReturnFalse")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productId = "Some test string";
            var url = "/images/users/2019-view_6.jpg";
            //Act 
            var actual = await productService.EditPicturePath(productId, url);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        //AddPicturePath
        [Fact]
        public async Task AddPicturePathShouldReturnTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                        .UseInMemoryDatabase("AddPicturePathShouldReturnTrue")
                        .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productId = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87";
            var url = "/images/users/2019-view_6.jpg";
            //Act 
            var actual = await productService.AddPicturePath(productId, url);
            var expected = true;
            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task AddPicturePathPassEmptyStrignShouldReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                          .UseInMemoryDatabase("AddPicturePathPassEmptyStrignShouldReturnFalse")
                          .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productId = "";
            var url = "/images/users/2019-view_6.jpg";
            //Act 
            var actual = await productService.AddPicturePath(productId, url);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddPicturePathPassNotExistingIdShouldReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                           .UseInMemoryDatabase("AddPicturePathPassNotExistingIdShouldReturnFalse")
                           .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productId = "eafb1980-50c2-4d54-b57f-c8443b5b8cfc";
            var url = "/images/users/2019-view_6.jpg";
            //Act 
            var actual = await productService.AddPicturePath(productId, url);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetProductById
        [Fact]
        public async Task GetProductByIdWithCorrectIdSouldReturnProductType()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                           .UseInMemoryDatabase("GetProductByIdWithCorrectIdSouldReturnProductType")
                           .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "c6172578-1164-4480-b3f5-790e1e25bfbb" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productId = "c6172578-1164-4480-b3f5-790e1e25bfbb";
            //Act 
            var productResult = await productService.GetProductById(productId);
            var actual = product.GetType();
            var expected = typeof(Product);
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetProductByIdWithCorrectIdSouldReturnProductNameSameAsPresent()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                           .UseInMemoryDatabase("GetProductByIdWithCorrectIdSouldReturnProductNameSameAsPresent")
                           .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "43b1ef57-fb90-4d86-8798-295916c1cac4", Name = "Georgi" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productId = "43b1ef57-fb90-4d86-8798-295916c1cac4";
            //Act 
            var productResult = await productService.GetProductById(productId);
            var actual = productResult.Name;
            var expected = product.Name;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetProductByIdWithInCorrectIdSouldReturnNull()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                            .UseInMemoryDatabase("GetProductByIdWithInCorrectIdSouldReturnNull")
                            .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87", Name = "Georgi" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productId = "f2de5725-6a76-48e3-bdc0-a142e686ce11"; //incorect id
            //Act 
            var actual = await productService.GetProductById(productId);
            Product expected = null;
            //Assert
            Assert.Equal(expected, actual);
        }

        //AddCategory
        [Fact]
        public async Task AddCategoryCorrectInputShouldReturnTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                            .UseInMemoryDatabase("AddCategoryCorrectInputShouldReturnTrue")
                            .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productId = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87";
            var category = new Category();
            //Act 
            var actual = await productService.AddCategory(productId, category);
            var expected = true;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddCategoryInCorrectInputShouldReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                            .UseInMemoryDatabase("AddCategoryInCorrectInputShouldReturnFalse")
                            .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productId = "e379816b-0b33-415f-a067-ea03095babf5";
            var category = new Category();
            //Act 
            var actual = await productService.AddCategory(productId, category);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetAllProducts
        [Fact]
        public void GetAllProductsShouldReturnCountOne()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                            .UseInMemoryDatabase("GetAllProductsShouldReturnCountOne")
                            .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87", Name = "Ivan", Price = 12.3m, PublishDate = DateTime.UtcNow };
            product.Pictures.Add(picture);
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            //Act 
            var result = productService.GetAllProducts<AllProductViewModel>().ToList();
            var actual = result.Count;
            var expected = 1;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllProductsShouldReturnFirstProductCorrectName()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                            .UseInMemoryDatabase("GetAllProductsShouldReturnFirstProductCorrectName")
                            .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);
            
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87", Name = "Product12", Price = 12.3m, PublishDate = DateTime.UtcNow };
            product.Pictures.Add(picture);
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
           
            //Act 
            var result = productService.GetAllProducts<AllProductViewModel>();
            var actual = result.First().Name;
            var expected = "Product12";
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllProductsShouldNotHavePresentsModels()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetAllProductsShouldNotHavePresentsModels")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);
            //Act 
            var actual = productService.GetAllProducts<AllProductViewModel>().Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        //EditProduct
        [Fact]
        public async Task EditProductCorrectInputSholdReturnProductType()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                              .UseInMemoryDatabase("EditProductCorrectInputSholdReturnProductType")
                              .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "e379816b-0b33-415f-a067-ea03095babf5", Name = "Georgi" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            //Act 
            var result = await productService.EditProduct(product);
            var actual = result.GetType();
            var expected = typeof(Product);
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EditProductCorrectInputSholdReturnProductWithChangeName()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("EditProductCorrectInputSholdReturnProductWithChangeName")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "e379816b-0b33-415f-a067-ea03095babf5", Name = "Georgi" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var inputProduct = dbContext.Products.Where(x => x.Id == product.Id).Single();
            inputProduct.Name = "Ivan";
            //Act 
            var result = await productService.EditProduct(inputProduct);
            var actual = result.Name;
            var expected = "Ivan";
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EditProductwithInCorrectInputSholdReturnNull()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                            .UseInMemoryDatabase("EditProductwithInCorrectInputSholdReturnNull")
                            .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var product = new Product() { Id = "e379816b-0b33-415f-a067-ea03095babf5", Name = "Georgi" };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            Product inputProduct = null;
            //Act 
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => productService.EditProduct(inputProduct));
        }

        //GetMyProducts
        [Fact]
        public void GetMyProductsShouldReturnCorrectCount()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                           .UseInMemoryDatabase("GetMyProductsShouldReturnCorrectCount")
                           .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var user = new MarketplaceUser() { Id = "62dc58fb-1f0e-4cbb-8b72-9f7f82f801cb", FirstName = "Ivan" };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var testProduct = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87", Name = "Product1", Price = 12.3m};
            testProduct.Pictures.Add(picture);
            dbContext.Products.Add(testProduct);
            dbContext.SaveChanges();

            user.Products.Add(testProduct);
            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            var userId = "62dc58fb-1f0e-4cbb-8b72-9f7f82f801cb";
            //Act 
            var result = productService.GetMyProducts<MyProductViewModel>(userId);
            var actual = result.Count();
            var expected = 1;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetMyProductsShouldReturnFirstProductModelCorrectName()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                            .UseInMemoryDatabase("GetMyProductsShouldReturnFirstProductModelCorrectName")
                            .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var user = new MarketplaceUser() { Id = "62dc58fb-1f0e-4cbb-8b72-9f7f82f801cb", FirstName = "Ivan" };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var testProduct = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87", Name = "Product1", Price = 12.3m };
            testProduct.Pictures.Add(picture);
            dbContext.Products.Add(testProduct);
            dbContext.SaveChanges();

            user.Products.Add(testProduct);
            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            var userId = "62dc58fb-1f0e-4cbb-8b72-9f7f82f801cb";
            //Act 
            var result = productService.GetMyProducts<MyProductViewModel>(userId);
            var actual = result.First().Name;
            var expected = "Product1";
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetMyProductsInputIncorectUserIdShouldReturnCountZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                              .UseInMemoryDatabase("GetMyProductsInputIncorectUserIdShouldReturnCountZero")
                              .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var user = new MarketplaceUser() { Id = "62dc58fb-1f0e-4cbb-8b72-9f7f82f801cb", FirstName = "Ivan" };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var testProduct = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87", Name = "Product1", Price = 12.3m };
            testProduct.Pictures.Add(picture);
            dbContext.Products.Add(testProduct);
            dbContext.SaveChanges();

            user.Products.Add(testProduct);
            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            var userId = "80421dbe-681c-48f8-9bc6-26da112da764"; //not existing userId
            //Act 
            var result = productService.GetMyProducts<MyProductViewModel>(userId);
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetMyProductsIncorectInputShouldReturnCountZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                              .UseInMemoryDatabase("GetMyProductsIncorectInputShouldReturnCountZero")
                              .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var user = new MarketplaceUser() { Id = "62dc58fb-1f0e-4cbb-8b72-9f7f82f801cb", FirstName = "Ivan" };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var testProduct = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87", Name = "Product1", Price = 12.3m };
            testProduct.Pictures.Add(picture);
            dbContext.Products.Add(testProduct);
            dbContext.SaveChanges();

            user.Products.Add(testProduct);
            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            var userId = ""; 
            //Act 
            var result = productService.GetMyProducts<MyProductViewModel>(userId);
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetProductsByCategoryId
        [Fact]
        public void GetProductsByCategoryIdCorrectInputShouldReturnCountOne()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductsByCategoryIdCorrectInputShouldReturnCountOne")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "7d004d74-3412-4b0f-b804-b018cb84f7c0", Name = "Product1", Price = 12.3m, PublishDate = DateTime.UtcNow , CategoryId = "62416150-6b4c-42ab-9d76-5c9559371bbd" };
            product.Pictures.Add(picture);
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var categoryId = "62416150-6b4c-42ab-9d76-5c9559371bbd";
            //Act 
            var result = productService.GetProductsByCategoryId<CategoriesProductViewModel>(categoryId);
            var actual = result.Count();
            var expected = 1;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetProductsByCategoryIdCorrectInputShouldReturnCorrectPrice()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductsByCategoryIdCorrectInputShouldReturnCorrectPrice")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "4c2af4ac-32ee-483b-8d3a-5d3ca5f10e87", Name = "Product1", Price = 12.3m, PublishDate = DateTime.UtcNow, CategoryId = "80421dbe-681c-48f8-9bc6-26da112da764" };
            product.Pictures.Add(picture);
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var categoryId = "80421dbe-681c-48f8-9bc6-26da112da764";
            //Act 
            var result = productService.GetProductsByCategoryId<CategoriesProductViewModel>(categoryId);
            var actual = result.First().Price;
            var expected = 12.3m;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetProductsByCategoryIdInCorrectInputShouldReturnCountZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductsByCategoryIdInCorrectInputShouldReturnCountZero")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "b3bce992-7185-47f0-bc8c-3816a51c1018", Name = "Product1", Price = 12.3m, PublishDate = DateTime.UtcNow, CategoryId = "80421dbe-681c-48f8-9bc6-26da112da764" };
            product.Pictures.Add(picture);
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var categoryId = "";
            //Act 
            var result = productService.GetProductsByCategoryId<CategoriesProductViewModel>(categoryId);
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetProductByInputAndCategoryName
        [Fact]
        public void GetProductByInputAndCategoryNameCorrectInputShouldReturnCountEqualToOne()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductByInputAndCategoryNameCorrectInputShouldReturnCountEqualToOne")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var category = new Category() { Id = "80421dbe-681c-48f8-9bc6-26da112da764", Name = "Smartphones" };
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "b3bce992-7185-47f0-bc8c-3816a51c1018", Name = "Samsung", Price = 12.3m, PublishDate = DateTime.UtcNow};
            product.Pictures.Add(picture);
            product.Category = category;
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var input = "a";
            var categoryName = "Smartphones";
            //Act 
            var result = productService.GetProductByInputAndCategoryName<HomeSearchViewModel>(input, categoryName );
            var actual = result.Count();
            var expected = 1;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetProductByInputAndCategoryNameCorrectInputShouldReturnNameSamsung()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductByInputAndCategoryNameCorrectInputShouldReturnNameSamsung")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var category = new Category() { Id = "80421dbe-681c-48f8-9bc6-26da112da764", Name = "Smartphones" };
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "b3bce992-7185-47f0-bc8c-3816a51c1018", Name = "Samsung", Price = 12.3m, PublishDate = DateTime.UtcNow };
            product.Pictures.Add(picture);
            product.Category = category;
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var input = "a";
            var categoryName = "Smartphones";
            //Act 
            var result = productService.GetProductByInputAndCategoryName<HomeSearchViewModel>(input, categoryName);
            var actual = result.First().Name;
            var expected = "Samsung";
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetProductByInputAndCategoryNameWithNotContainingInputShouldReturnModelWithCountZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductByInputAndCategoryNameWithNotContainingInputShouldReturnModelWithCountZero")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var category = new Category() { Id = "80421dbe-681c-48f8-9bc6-26da112da764", Name = "Smartphones" };
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "b3bce992-7185-47f0-bc8c-3816a51c1018", Name = "Samsung", Price = 12.3m, PublishDate = DateTime.UtcNow };
            product.Pictures.Add(picture);
            product.Category = category;
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var input = "z";
            var categoryName = "Smartphones";
            //Act 
            var result = productService.GetProductByInputAndCategoryName<HomeSearchViewModel>(input, categoryName);
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetProductByInputAndCategoryNameWithNotContainingCategoryInputShouldReturnModelWithCountZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductByInputAndCategoryNameWithNotContainingCategoryInputShouldReturnModelWithCountZero")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var category = new Category() { Id = "80421dbe-681c-48f8-9bc6-26da112da764", Name = "Smartphones" };
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "b3bce992-7185-47f0-bc8c-3816a51c1018", Name = "Samsung", Price = 12.3m, PublishDate = DateTime.UtcNow };
            product.Pictures.Add(picture);
            product.Category = category;
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var input = "a";
            var categoryName = "Laptops";
            //Act 
            var result = productService.GetProductByInputAndCategoryName<HomeSearchViewModel>(input, categoryName);
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetProductByInput
        [Fact]
        public void GetProductByInputWithCorrectInputShouldReturnOneModel()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductByInputWithCorrectInputShouldReturnOneModel")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "b3bce992-7185-47f0-bc8c-3816a51c1018", Name = "Samsung", Price = 12.3m, PublishDate = DateTime.UtcNow };
            product.Pictures.Add(picture);
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var input = "a";
            //Act 
            var result = productService.GetProductByInput<HomeSearchViewModel>(input);
            var actual = result.Count();
            var expected = 1;
            //Assert
            Assert.Equal(expected, actual);
            Assert.Equal(12.3m, result.First().Price);
        }

        [Fact]
        public void GetProductByInputWithInCorrectInputShouldReturnZeroModels()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductByInputWithInCorrectInputShouldReturnZeroModels")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "b3bce992-7185-47f0-bc8c-3816a51c1018", Name = "Samsung", Price = 12.3m, PublishDate = DateTime.UtcNow };
            product.Pictures.Add(picture);
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var input = "dddd";
            //Act 
            var result = productService.GetProductByInput<HomeSearchViewModel>(input);
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetProductByCategoryName
        [Fact]
        public void GetProductByCategoryNameCorrectInputShouldReturnOneModel()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductByCategoryNameCorrectInputShouldReturnOneModel")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var category = new Category() { Id = "80421dbe-681c-48f8-9bc6-26da112da764", Name = "Smartphones" };
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "b3bce992-7185-47f0-bc8c-3816a51c1018", Name = "Samsung", Price = 12.3m, PublishDate = DateTime.UtcNow };
            product.Pictures.Add(picture);
            product.Category = category;
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var categoryName = "Smartphones";
            //Act 
            var result = productService.GetProductByCategoryName<HomeSearchViewModel>(categoryName);
            var actual = result.Count();
            var expected = 1;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetProductByCategoryNameWithEmptyCategoryNametShouldReturnZeroModels()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                             .UseInMemoryDatabase("GetProductByCategoryNameWithEmptyCategoryNametShouldReturnZeroModels")
                             .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var productService = new ProductService(dbContext, mapper);

            var category = new Category() { Id = "80421dbe-681c-48f8-9bc6-26da112da764", Name = "Smartphones" };
            var picture = new Picture() { PictureUrl = "/images/users/2019-view_6.jpg" };
            var product = new Product() { Id = "b3bce992-7185-47f0-bc8c-3816a51c1018", Name = "Samsung", Price = 12.3m, PublishDate = DateTime.UtcNow };
            product.Pictures.Add(picture);
            product.Category = category;
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var categoryName = "";
            //Act 
            var result = productService.GetProductByCategoryName<HomeSearchViewModel>(categoryName);
            var actual = result.Count();
            var expected = 0;
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
