
using BREAD.Controllers;
using Mahasiswa.Application.Services;
using Mahasiswa.Domain.Entities;
using Mahasiswa.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

using Moq;
namespace UnitTesting.ControllersTest
{
    public class MahasiswaControllerTests
    {
        #region helper
        public MahasiswaControllers CreateControllersWithMock(out Mock<IMahasiswaServices> mockServices)
        {
            mockServices = new Mock<IMahasiswaServices>();
            return new MahasiswaControllers(mockServices.Object);
        }
        #endregion

        #region Browse Mahasiswa By Id
        [Fact]
        public async Task BrowseMahasiswaByNIM_ReturnOkeResult_ReturnsData()
        {
            //arange
            var controllers = CreateControllersWithMock(out var mockServices);
            mockServices.Setup(r => r.BrowseMahasiswaByNIM("2210817120013")).ReturnsAsync(
                new MahasiswaData
                {
                    Name = "Fathiah",
                    NIM = "2210817120013",
                    isActive = true,
                });

            //act
            var result = await controllers.BrowseByID("2210817120013");

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("2210817120013", ((MahasiswaData)((OkObjectResult)result).Value).NIM);

        }
        [Fact]
        public async Task BrowseMahasiswaByNIM_InvalidNIM_ReturnsNotFound()
        {   //arange
            var controllers = CreateControllersWithMock(out var mockServices);
            mockServices.Setup(r => r.BrowseMahasiswaByNIM("2210817120009")).ReturnsAsync((MahasiswaData)null);

            //act
            var result = await controllers.BrowseByID("2210817120009");

            //assert
            Assert.IsType<NotFoundResult>(result);

        }
        [Fact]
        public async Task BrowseMahasiswaByNIM_EmptyNIM_ReturnsNotFound()
        {
            //arange
            var controllers = CreateControllersWithMock(out var mockServices);
            mockServices.Setup(r => r.BrowseMahasiswaByNIM(" ")).ReturnsAsync((MahasiswaData)null);

            //act
            var result = await controllers.BrowseByID(" ");

            //assert
            Assert.IsType<NotFoundResult>(result);

        }
        #endregion

        #region Read Mahasiswa
        [Fact]
        public async Task ReadMahasiswa_ReturnOkeResult_ReturnsData()
        {
            //arange
            var controllers = CreateControllersWithMock(out var mockServices);
            mockServices.Setup(r => r.ReadMahasiswa()).ReturnsAsync(
                new List<MahasiswaData>
                { new MahasiswaData
                {
                    Name = "Fathiah",
                    NIM = "2210817120013",
                    isActive = true,
                }
                });

            //act
            var result = await controllers.ReadAllMahasiswa();

            //assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var items = Assert.IsAssignableFrom<IEnumerable<MahasiswaData>>(okResult.Value);
            Assert.NotEmpty(items);
        }

        [Fact]
        public async Task ReadMahasiswa_ReturnsOkWithEmptyList()
        {
            //arange
            var controllers = CreateControllersWithMock(out var mockServices);
            mockServices.Setup(r => r.ReadMahasiswa()).ReturnsAsync(new List<MahasiswaData>());

            //act
            var result = await controllers.ReadAllMahasiswa();

            //assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var items = Assert.IsAssignableFrom<IEnumerable<MahasiswaData>>(okResult.Value);
            Assert.Empty(items);
        }
        //WOIIIIIIIIIIIIIIIIIIIIIII SATUUUUUUUUUUUUUU LAGIIIIIIIIIIIIIIII SCENARIOOOOOOOOOOOOOO
        #endregion

        #region Update Mahasiswa By ID
        [Fact]
        public async Task UpdateMahasiswaByID_ValidIdAndData_ReturnsNoContent()
        {
            //arange
            var controllers = CreateControllersWithMock(out var mockServices);
            var mahasiswa = new MahasiswaData
            {
                NIM = "2210817120009",
                Name = "Fathiah Nuraisyah",
                isActive = false,
            };
            mockServices.Setup(r => r.UpdateMahasiswaByID(1, mahasiswa)).ReturnsAsync(1);

            //act
            var result = await controllers.UpdateMahasiswa(1, mahasiswa);

            //assert
            Assert.IsType<NoContentResult>(result);
            
        }
        [Fact]
        public async Task UpdateMahasiswaByID_InvalidId_ReturnsNotFound()
        {
            //arange
            var controllers = CreateControllersWithMock(out var mockServices);
            var mahasiswa = new MahasiswaData
            {
                NIM = "2210817120009",
                Name = "Fathiah Nuraisyah",
                isActive = false,
            };
            mockServices.Setup(r => r.UpdateMahasiswaByID(999, mahasiswa)).ReturnsAsync(0);

            //act
            var result = await controllers.UpdateMahasiswa(999, mahasiswa);

            //assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task UpdateMahasiswaByID_EmptyData_ReturnsNotFound()
        {
            //arange
            var controllers = CreateControllersWithMock(out var mockServices);
            var mahasiswa = new MahasiswaData();
            mockServices.Setup(r => r.UpdateMahasiswaByID(1, mahasiswa)).ReturnsAsync(0);

            //act
            var result = await controllers.UpdateMahasiswa(1, mahasiswa);

            //assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Delete Mahasiswa By ID
        [Fact]
        public async Task DeleteMahasiswaByID_ValidId_ReturnsNoContent()
        {
            // Arrange
            var controllers = CreateControllersWithMock(out var mockServices);
            mockServices.Setup(r => r.DeleteMahasiswaByID(1)).ReturnsAsync(1);

            // Act
            var result = await controllers.DeleteByID(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteMahasiswaByID_inValidId_ReturnsNotFound()
        {
            // Arrange
            var controllers = CreateControllersWithMock(out var mockServices);
            mockServices.Setup(r => r.DeleteMahasiswaByID(999)).ReturnsAsync(0);

            // Act
            var result = await controllers.DeleteByID(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task DeleteMahasiswaByID_NegativeId__ReturnsNotFound()
        {
            // Arrange
            var controllers = CreateControllersWithMock(out var mockServices);
            mockServices.Setup(r => r.DeleteMahasiswaByID(-1)).ReturnsAsync(0);

            // Act
            var result = await controllers.DeleteByID(-1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Add Mahasiswa
        [Fact]
        public async Task AddMahasiswa_ValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var controllers = CreateControllersWithMock(out var mockServices);
            var mahasiswa = new MahasiswaData
            {
                Name = "Fathiah",
                NIM = "2210817120013",
                isActive = true,
            };
            mockServices.Setup(s => s.AddMahasiswa(mahasiswa)).ReturnsAsync(1);

            // Act
            var result = await controllers.AddMahasiswa(mahasiswa);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("BrowseByID", createdResult.ActionName);
            Assert.Equal(mahasiswa.NIM, ((MahasiswaData)createdResult.Value).NIM);
        }

        [Fact]
        public async Task AddMahasiswa_NullData_ReturnsBadRequest()
        {
            // Arrange
            var controllers = CreateControllersWithMock(out var mockServices);

            // Act
            var result = await controllers.AddMahasiswa(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task AddMahasiswa_FailedInsert_ReturnsBadRequest()
        {
            // Arrange
            var controllers = CreateControllersWithMock(out var mockServices);
            var mahasiswa = new MahasiswaData
            {
                Name = "Fathiah",
                NIM = "2210817120013",
                isActive = true,
            };
            mockServices.Setup(s => s.AddMahasiswa(mahasiswa)).ReturnsAsync(0);

            // Act
            var result = await controllers.AddMahasiswa(mahasiswa);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

    }
}
