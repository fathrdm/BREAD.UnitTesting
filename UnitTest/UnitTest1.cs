using Xunit;
using BREAD.Controllers;
using BREAD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BREAD.Tests
{
    public class BrandControllerTests
    {
        private DbContextOptions<BrandContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<BrandContext>()
                .UseInMemoryDatabase(databaseName: "BrandTestDB")
                .Options;
        }

        [Fact]
        public async Task GetBrand_ReturnsListOfBrands()
        {
            // Arrange
            var options = GetInMemoryOptions();

            using (var context = new BrandContext(options))
            {
                context.Brands.Add(new Brand { Id = 1, Name = "Brand A", NIM = "123", isActive = 1 });
                context.Brands.Add(new Brand { Id = 2, Name = "Brand B", NIM = "456", isActive = 1 });
                context.SaveChanges();
            }

            using (var context = new BrandContext(options))
            {
                var controller = new BrandController(context);

                // Act
                var result = await controller.GetBrand();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<Brand>>>(result);
                var okResult = Assert.IsAssignableFrom<IEnumerable<Brand>>(actionResult.Value);
                Assert.Equal(2, okResult.Count());
            }
        }

        [Fact]
        public async Task GetBrandById_ReturnsCorrectBrand()
        {
            var options = GetInMemoryOptions();

            using (var context = new BrandContext(options))
            {
                context.Brands.Add(new Brand { Id = 1, Name = "Brand X", NIM = "789", isActive = 1 });
                context.SaveChanges();
            }

            using (var context = new BrandContext(options))
            {
                var controller = new BrandController(context);
                var result = await controller.GetBrandById(1);

                var actionResult = Assert.IsType<ActionResult<Brand>>(result);
                var brand = Assert.IsType<Brand>(actionResult.Value);
                Assert.Equal("Brand X", brand.Name);
            }
        }

        [Fact]
        public async Task PostBrand_AddsNewBrand()
        {
            var options = GetInMemoryOptions();

            using (var context = new BrandContext(options))
            {
                var controller = new BrandController(context);
                var newBrand = new Brand { Id = 3, Name = "Brand New", NIM = "999", isActive = 1 };

                var result = await controller.PostBrand(newBrand);

                var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
                var brand = Assert.IsType<Brand>(createdAt.Value);
                Assert.Equal("Brand New", brand.Name);
            }
        }

        [Fact]
        public async Task DeleteBrand_RemovesExistingBrand()
        {
            var options = GetInMemoryOptions();

            using (var context = new BrandContext(options))
            {
                context.Brands.Add(new Brand { Id = 10, Name = "To Delete", NIM = "000", isActive = 1 });
                context.SaveChanges();
            }

            using (var context = new BrandContext(options))
            {
                var controller = new BrandController(context);
                var result = await controller.DeleteBrand(10);

                Assert.IsType<OkResult>(result);
                Assert.Empty(context.Brands);
            }
        }
    }
}
