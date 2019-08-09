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
            var actual  = result.Count();
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
    }
}
