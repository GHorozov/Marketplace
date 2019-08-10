using AutoMapper;
using Marketplace.App.AutoMapperConfigurations;
using Marketplace.App.Infrastructure;
using Marketplace.Data;
using Marketplace.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Services.Tests
{
    public class PictureServiceTests
    {
        //[Fact]
        //public async Task SavePictureCorrectInputReturnsCorrectPath()
        //{
        //    //Arrange
        //    var options = new DbContextOptionsBuilder<MarketplaceDbContext>()
        //                   .UseInMemoryDatabase("AddProductCheckQuantityWithInMemoryDatabase")
        //                   .Options;
        //    var dbContext = new MarketplaceDbContext(options);
        //    var profile = new MarketplaceProfile();
        //    var configuration = new MapperConfiguration(x => x.AddProfile(profile));
        //    var mapper = new Mapper(configuration);
        //    var productService = new ProductService(dbContext, mapper);
        //    var pictureService = new PictureService(productService);

        //    var product = new Product() { Id = "5b928359-2125-4e5a-9cf3-6fd6b4d2aadd",  };
        //    dbContext.Products.Add(product);
        //    await dbContext.SaveChangesAsync();
        //    var picture = new Picture() { Id = "a0b5912d-2aa5-4f14-ae03-9f34a96162fa", PictureUrl = "", ProductId = product.Id};
        //    product.Pictures.Add(picture);
        //    await dbContext.SaveChangesAsync();

        //    var productId = product.Id;
        //    var inputPath = "blog_1.jpg";
        //    FormFile file = null;
        //    using (var stream = File.OpenRead(inputPath))
        //    {
        //        file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
        //        {
        //            Headers = new HeaderDictionary(),
        //            ContentType = "image/jpeg"
        //        };
        //    }
        //    var defaultPath = "";
        //    //Act 
        //    var actual = await pictureService.SavePicture(productId, file, defaultPath);
        //    var expected = "/images/users/2019-blog_1.jpg";
        //    //Assert
        //    Assert.Equal(expected, actual);
        //}

    }
}
