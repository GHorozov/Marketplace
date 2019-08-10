using AutoMapper;
using Marketplace.App.AutoMapperConfigurations;
using Marketplace.App.ViewModels.Components;
using Marketplace.Data;
using Marketplace.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Services.Tests
{
    public class CategoryServiceTests
    {
        //GetAllCategories
        [Fact]
        public void GetAllCategoriesWithPresentCategoriesShouldExist()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetAllCategoriesWithPresentCategoriesShouldExist")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var category1 = new Category() { Id = "42a27ec6-3abb-4e7b-a224-6fd873564368", Name = "Smartphones" };
            var category2 = new Category() { Id = "7821b986-e16a-4a50-99c5-e5d0dc71488d", Name = "Laptops" };

            dbContext.Categories.Add(category1);
            dbContext.Categories.Add(category2);
            dbContext.SaveChanges();

            //Act 
            var result = categoryService.GetAllCategories<IndexCategoryViewModel>();
            var actual = result.Count();
            var expected = 2;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllCategoriesInputTwoCategoriesReturnFirstNameAnIdShouldMatch()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetAllCategoriesInputTwoCategoriesReturnFirstNameShouldMatch")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var category1 = new Category() { Id = "42a27ec6-3abb-4e7b-a224-6fd873564368", Name = "Smartphones" };
            var category2 = new Category() { Id = "7821b986-e16a-4a50-99c5-e5d0dc71488d", Name = "Laptops" };

            dbContext.Categories.Add(category1);
            dbContext.Categories.Add(category2);
            dbContext.SaveChanges();

            //Act 
            var result = categoryService.GetAllCategories<IndexCategoryViewModel>();
            var actual = result.First().Name;
            var expected = "Smartphones";
            //Assert
            Assert.Equal(expected, actual);
            Assert.Equal("42a27ec6-3abb-4e7b-a224-6fd873564368", result.First().CategoryId);
        }

        //Create
        [Fact]
        public async Task CreateWithCorretInputShouldReturnTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("CreateWithCorretInputShouldReturnTrue")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var CategoryName = "Computers";
            //Act 
            var actual = await categoryService.Create(CategoryName);
            var expected = true;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateWithInCorretInputShouldReturnFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("CreateWithInCorretInputShouldReturnFalse")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var CategoryName = " ";
            //Act 
            var actual = await categoryService.Create(CategoryName);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetCategoryById
        [Fact]
        public async Task GetCategoryByIdWithCorectIdreturnsCategoryType()
        { 
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetCategoryByIdWithCorectIdreturnsCategory")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var category = new Category() { Id = "42a27ec6-3abb-4e7b-a224-6fd873564368", Name = "Smartphones" };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var categoryId = "42a27ec6-3abb-4e7b-a224-6fd873564368"; 
            //Act 
            var result = await categoryService.GetCategoryById(categoryId);
            var actual = result.GetType();
            var expected = typeof(Category);
            //Assert
            Assert.Equal(expected, actual);
            Assert.Equal("Smartphones", result.Name);
        }

        [Fact]
        public async Task GetCategoryByIdinputStringEmptyReturnsNull()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetCategoryByIdinputStringEmptyReturnsNull")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var category = new Category() { Id = "42a27ec6-3abb-4e7b-a224-6fd873564368", Name = "Smartphones" };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var categoryId = "";
            //Act 
            var actual = await categoryService.GetCategoryById(categoryId);
            Category expected = null;
            //Assert
            Assert.Equal(expected, actual);
        }

        //GetCategoryByName
        [Fact]
        public async Task GetCategoryByNameCorrectinputReturnsCategoryType()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetCategoryByNameCorrectinputReturnsCategoryType")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var category = new Category() { Id = "42a27ec6-3abb-4e7b-a224-6fd873564368", Name = "Smartphones" };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var categoryName = "Smartphones";
            //Act 
            var result = await categoryService.GetCategoryByName(categoryName);
            var actual = result.GetType();
            var expected = typeof(Category);
            //Assert
            Assert.Equal(expected, actual);
            Assert.Equal("42a27ec6-3abb-4e7b-a224-6fd873564368", result.Id);
        }

        [Fact]
        public async Task GetCategoryByNameInCorrectinputReturnsNull()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("GetCategoryByNameInCorrectinputReturnsNull")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var category = new Category() { Id = "42a27ec6-3abb-4e7b-a224-6fd873564368", Name = "Smartphones" };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var categoryName = "";
            //Act 
            var actual = await categoryService.GetCategoryByName(categoryName);
            Category expected = null;
            //Assert
            Assert.Equal(expected, actual);
        }

        //Edit
        [Fact]
        public async Task EditWithCorectInputReturnsTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("EditWithCorectInputReturnsTrue")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var category = new Category() { Id = "42a27ec6-3abb-4e7b-a224-6fd873564368", Name = "Smartphones" };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var id = "42a27ec6-3abb-4e7b-a224-6fd873564368";
            var name = "newName";
            //Act 
            var actual = await categoryService.Edit(id, name);
            var expected = true;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EditWithInCorectInputEmptyIdReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("EditWithInCorectInputEmptyIdReturnsFalse")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var category = new Category() { Id = "42a27ec6-3abb-4e7b-a224-6fd873564368", Name = "Smartphones" };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var id = " ";
            var name = "newName";
            //Act 
            var actual = await categoryService.Edit(id, name);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EditWithInCorectInputEmptyNameReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
                         .UseInMemoryDatabase("EditWithInCorectInputEmptyNameReturnsFalse")
                         .Options;
            var dbContext = new MarketplaceDbContext(options);
            var profile = new MarketplaceProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var categoryService = new CategoryService(dbContext, mapper);

            var category = new Category() { Id = "42a27ec6-3abb-4e7b-a224-6fd873564368", Name = "Smartphones" };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var id = "42a27ec6-3abb-4e7b-a224-6fd873564368";
            var name = " ";
            //Act 
            var actual = await categoryService.Edit(id, name);
            var expected = false;
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
