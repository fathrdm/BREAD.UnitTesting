using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahasiswa.Application.Services;
using Mahasiswa.Domain.Interface;
using Mahasiswa.Domain.Entities;
using Moq;
using UnitTesting.Helpers;

namespace UnitTesting.ServicesTest
{
    public class MahasiswaServicesTest
    {
        #region helper
        public MahasiswaServices CreateServiceWithMock(out Mock<IMahasiswaRepository> mockRepo)
        {
            mockRepo = new Mock<IMahasiswaRepository>();
            return new MahasiswaServices(mockRepo.Object);
        }

        #endregion

        #region Browse Mahasiswa By NIM
        [Fact]
        public async Task BrowseMahasiswaByNIM_ValidNIM_ReturnsData()
        {
            //arange
            var service = CreateServiceWithMock(out var mockRepo);
            mockRepo.Setup(r => r.BrowseMahasiswaByNIM("2210817120013")).ReturnsAsync(
                new MahasiswaData
                {
                    Name = "Fathiah",
                    NIM = "2210817120013",
                    isActive = true,
                });

            //act
            var result = await service.BrowseMahasiswaByNIM("2210817120013");

            //assert
            Assert.Equal("2210817120013", result.NIM);
            mockRepo.Verify(r => r.BrowseMahasiswaByNIM("2210817120013"), Times.Once);
        }
        [Fact]
        public async Task BrowseMahasiswaByNIM_InvalidNIM_ReturnsNull()
        {
            //arange
            var service = CreateServiceWithMock(out var mockRepo);
            mockRepo.Setup(r => r.BrowseMahasiswaByNIM("2210817120009")).ReturnsAsync((MahasiswaData)null);

            //act
            var result = await service.BrowseMahasiswaByNIM("2210817120009");

            //assert
            Assert.Null(result);
        }
        [Fact]
        public async Task BrowseMahasiswaByNIM_EmptyOrWhitespaceNIM_ReturnsNull()
        {
            //arange
            var service = CreateServiceWithMock(out var mockRepo);
            mockRepo.Setup(r => r.BrowseMahasiswaByNIM(" ")).ReturnsAsync((MahasiswaData)null);

            //act
            var result = await service.BrowseMahasiswaByNIM(" ");

            //assert
            Assert.Null(result);
        }

        #endregion

        #region Read Mahasiswa
        [Fact]
        public async Task ReadMahasiswa_ReturnsAllData()
        {
            //arange
            var service = CreateServiceWithMock(out var mockRepo);
            mockRepo.Setup(r => r.ReadMahasiswa()).ReturnsAsync(new List<MahasiswaData>
            {
                new MahasiswaData { Id = 1, Name = "Fathiah Nuraisyah Radam", NIM = "2210817120013", isActive = true }
            });

            //act
            var result = await service.ReadMahasiswa();

            //assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task ReadMahasiswa_EmptyData_ReturnsEmptyList()
        {
            //arange
            var service = CreateServiceWithMock(out var mockRepo);
            mockRepo.Setup(r => r.ReadMahasiswa()).ReturnsAsync(new List<MahasiswaData>());

            //act
            var result = await service.ReadMahasiswa();

            //assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        [Fact]
        public async Task ReadMahasiswa_ReturnsCorrectDataStructure()
        {
            //arange
            var service = CreateServiceWithMock(out var mockRepo);
            mockRepo.Setup(r => r.ReadMahasiswa()).ReturnsAsync(new List<MahasiswaData>());

            //act
            var result = await service.ReadMahasiswa();

            //assert
            Assert.All(result, item =>
            {
                Assert.False(string.IsNullOrEmpty(item.NIM), "NIM kosong");
                Assert.False(string.IsNullOrEmpty(item.Name), "Name kosong");
                Assert.NotNull(item.isActive);
            });
        }
        #endregion

        #region Update Mahasiswa By ID
        [Fact]
        public async Task UpdateMahasiswaByID_ValidIdAndData_Returns1()
        {
            //arange
            var service = CreateServiceWithMock(out var mockRepo);
            var mahasiswa = new MahasiswaData
            {
                NIM = "2210817120009",
                Name = "Fathiah Nuraisyah",
                isActive = false,
            };
            mockRepo.Setup(r => r.UpdateMahasiswaByID(1, mahasiswa)).ReturnsAsync(1);

            //act
            var result = await service.UpdateMahasiswaByID(1, mahasiswa);

            //assert
            Assert.Equal(1, result);
            mockRepo.VerifyAll();
        }

        [Fact]
        public async Task UpdateMahasiswaByID_InvalidId_Returns0()
        {
            //arange
            var service = CreateServiceWithMock(out var mockRepo);
            var mahasiswa = new MahasiswaData
            {
                NIM = "2210817120009",
                Name = "Fathiah Nuraisyah",
                isActive = false,
            };
            mockRepo.Setup(r => r.UpdateMahasiswaByID(999, mahasiswa)).ReturnsAsync(0);

            //act
            var result = await service.UpdateMahasiswaByID(999, mahasiswa);

            //assert
            Assert.Equal(0, result);
            mockRepo.VerifyAll();
        }

        [Fact]
        public async Task UpdateMahasiswaByID_EmptyData_Returns0()
        {
            //arange
            var service = CreateServiceWithMock(out var mockRepo);
            var mahasiswa = new MahasiswaData();
            mockRepo.Setup(r => r.UpdateMahasiswaByID(1, mahasiswa)).ReturnsAsync(0);

            //act
            var result = await service.UpdateMahasiswaByID(1, mahasiswa);

            //assert
            Assert.Equal(0, result);
            mockRepo.VerifyAll();
        }
        #endregion

        #region Delete Mahasiswa By ID
        [Fact]
        public async Task DeleteMahasiswaByID_ValidId_Returns1()
        {
            // Arrange
            var service = CreateServiceWithMock(out var mockRepo);
            mockRepo.Setup(r => r.DeleteMahasiswaByID(1)).ReturnsAsync(1);

            // Act
            var result = await service.DeleteMahasiswaByID(1);

            // Assert
            Assert.Equal(1, result);
        }
        [Fact]
        public async Task DeleteMahasiswaByID_inValidId_Returns0()
        {
            // Arrange
            var service = CreateServiceWithMock(out var mockRepo);
            mockRepo.Setup(r => r.DeleteMahasiswaByID(999)).ReturnsAsync(0);

            // Act
            var result = await service.DeleteMahasiswaByID(999);

            // Assert
            Assert.Equal(0, result);
            mockRepo.VerifyAll();
        }

        [Fact]
        public async Task DeleteMahasiswaByID_NegativeId_Returns0()
        {
            // Arrange
            var service = CreateServiceWithMock(out var mockRepo);
            mockRepo.Setup(r => r.DeleteMahasiswaByID(-999)).ReturnsAsync(0);

            // Act
            var result = await service.DeleteMahasiswaByID(-999);

            // Assert
            Assert.Equal(0, result);
            mockRepo.VerifyAll();
        }
        #endregion

        #region Add Mahasiswa
        [Fact]
        public async Task AddMahasiswa_ValidData_Returns1()
        {
            // Arrange
            var service = CreateServiceWithMock(out var mockRepo);
            var newMahasiswa = new MahasiswaData
            {
                Name = "Muhammad Fulan",
                NIM = "2210817120009",
                isActive = true
            };

            mockRepo.Setup(r => r.AddMahasiswa(newMahasiswa)).ReturnsAsync(1);

            // Act
            var result = await service.AddMahasiswa(newMahasiswa);

            // Assert
            Assert.Equal(1, result);
            mockRepo.Verify(r => r.AddMahasiswa(newMahasiswa), Times.Once);
        }

        [Fact]
        public async Task AddMahasiswa_NullData_ThrowsArgumentNullException()
        {
            // Arrange
            var service = CreateServiceWithMock(out var mockRepo);

            mockRepo.Setup(r => r.AddMahasiswa(null)).ReturnsAsync(0);

            // Act
            var result = await service.AddMahasiswa(null);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task AddMahasiswa_EmptyData_Returns0()
        {
            // Arrange
            var service = CreateServiceWithMock(out var mockRepo);
            var emptyMahasiswa = new MahasiswaData(); 

            mockRepo.Setup(r => r.AddMahasiswa(emptyMahasiswa)).ReturnsAsync(0);

            // Act
            var result = await service.AddMahasiswa(emptyMahasiswa);

            // Assert
            Assert.Equal(0, result);
            mockRepo.Verify(r => r.AddMahasiswa(emptyMahasiswa), Times.Once);
        }
        #endregion

    }
}
