using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;

namespace TestProject3DotNet
{
    public class ProductServiceTests
    {
        private P3Referential context;

        public ProductServiceTests()
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var configuration = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json").Build();

            string connexion = configuration.GetConnectionString("P3Referential");
            Console.WriteLine(connexion);
            var options = new DbContextOptionsBuilder<P3Referential>().UseSqlServer(connexion).Options;
            context = new P3Referential(options, configuration);
        }
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void Create_Product()
        {
            // Arrange
            var localisationOptions = Options.Create(new LocalizationOptions());
            var logger = LoggerFactory.Create(builder => builder.AddConsole());
            ResourceManagerStringLocalizerFactory factory = new ResourceManagerStringLocalizerFactory(localisationOptions, logger);
            var localizer = new StringLocalizer<ProductService>(factory);
            ProductService productService = new ProductService(new Cart(), new ProductRepository(context), new OrderRepository(context), localizer);
            LanguageService languageService = new LanguageService();
            ProductController productController = new ProductController(productService, languageService, localizer);

            // Act
            ProductViewModel product = new ProductViewModel { Name = "new product", Description = "test new product", Price = "10,5", Details = "", Stock = "1" };
            productController.Create(product);
            productService.SaveProduct(product);

            // Assert
            var insertedProduct = context.Product.FirstOrDefault();
            Assert.NotNull(insertedProduct);
        }

        [Fact]
        public void Delete_Product()
        {
            // Arrange
            var localisationOptions = Options.Create(new LocalizationOptions());
            var logger = LoggerFactory.Create(builder => builder.AddConsole());
            ResourceManagerStringLocalizerFactory factory = new ResourceManagerStringLocalizerFactory(localisationOptions, logger);
            var localizer = new StringLocalizer<ProductService>(factory);
            ProductService productService = new ProductService(new Cart(), new ProductRepository(context), new OrderRepository(context), localizer);
            LanguageService languageService = new LanguageService();
            //productService.SaveProduct(new Models.ViewModels.ProductViewModel { });
            ProductController productController = new ProductController(productService, languageService, localizer);
            var lastProduct = context.Product.OrderBy(p => p.Id).LastOrDefault();

            // Act & Assert
            Assert.NotNull(lastProduct);
            productController.DeleteProduct(lastProduct.Id);
            context.SaveChangesAsync();

            var productDeleted = context.Product.Find(lastProduct.Id);
            Assert.Null(productDeleted);
        }
    }
}
