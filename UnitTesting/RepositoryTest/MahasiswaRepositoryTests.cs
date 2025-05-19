using Xunit;
using Mahasiswa.Domain.Entities;
using Mahasiswa.Infrastructure.Repositories;
using UnitTesting.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitTesting.RepositoryTest
{
    public class MahasiswaRepositoryTests
    {
        #region Read Mahasiswa
        [Fact]
        public async Task ReadMahasiswa_ReturnsData()
        {
            //Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();

            //Act            
            var result = await repo.ReadMahasiswa();

            //Assert
            Assert.NotNull(result);             
            Assert.True(result.Count >= 1);
            Assert.Equal("Fathiah Nuraisyah Radam", result[0].Name);
        }

        [Fact]
        public async Task ReadMahasiswa_EmptyData_ReturnsEmptyList()
        {
            // Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithEmptyContext();

            // Act
            var result = await repo.ReadMahasiswa();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReadMahasiswa_MultipleData_ReturnsAllData()
        {
            // Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();

            // Act
            var result = await repo.ReadMahasiswa();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 1);
            //Assert.Contains(result, m => m.Name == "Fathiah Nuraisyah Radam");
            //Assert.Contains(result, m => m.Name == "Muhammad Fulan");
        }
        #endregion

        #region Browse Mahasiswa By NIM
        [Fact]
        public async Task BrowseMahasiswaByID_WhenDataExists_ReturnsData()
        {
            //Arange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();

            //Art
            var result = await repo.BrowseMahasiswaByNIM("2210817120013");

            //Assert
            Assert.NotNull(result);
            Assert.Equal("2210817120013", result.NIM);

        }
        [Fact]
        public async Task BrowseMahasiswaByID_inValidId_ReturnsNull()
        {
            //arange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();

            //act
            var result = await repo.BrowseMahasiswaByNIM("2210817120001");

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task BrowseMahasiswaByID_EmptyNIM_ReturnsNull()
        {
            //arange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();

            //act
            var result = await repo.BrowseMahasiswaByNIM(" ");

            //Assert
            Assert.Null(result);
        }
        #endregion

        #region Delete Mahasiswa By ID
        [Fact]
        public async Task DeleteMahasiswaByID_ExistingId_Returns1()
        {
            //arange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();

            // Act
            var delete = await repo.DeleteMahasiswaByID(1);

            // Assert
            Assert.Equal(1, delete);
        }
        [Fact]
        public async Task DeleteMahasiswaByID_inValidId_Returns0()
        {
            //arange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();

            // Act
            var delete = await repo.DeleteMahasiswaByID(3);

            // Assert
            Assert.Equal(0, delete);
        }
        [Fact]
        public async Task DeleteMahasiswaByID_NegativeId_Returns0()
        {
            // Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();

            // Act
            var delete = await repo.DeleteMahasiswaByID(-999);

            // Assert
            Assert.Equal(0, delete);
        }

        #endregion

        #region Update Mahasiswa By ID
        [Fact]
        public async Task UpdateMahasiswaByID_ValidData_ReturnsData()
        {
            // Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();
            var pastID = 1;

            var updatedMahasiswa = new MahasiswaData()
            {
                Id = pastID,
                Name = "Fathiah Nuraisyah Radam",
                NIM = "221",
                isActive = true
            };

            // Act
            var result = await repo.UpdateMahasiswaByID(pastID, updatedMahasiswa);

            // Assert
            Assert.True(result > 0, "Update operation should affect at least one record");

            var updatedData = await repo.BrowseMahasiswaByNIM(updatedMahasiswa.NIM);
            Assert.NotNull(updatedData);
            Assert.Equal("Fathiah Nuraisyah Radam", updatedData.Name);
            Assert.Equal("221", updatedData.NIM);
            Assert.True(updatedData.isActive);
        }

        [Fact]
        public async Task UpdateMahasiswaByID_invalidId_Returns0() 
        {
            // Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithSeededContext();
            var invalidID = 5;

            var updatedMahasiswa = new MahasiswaData()
            {
                Id = invalidID,
                Name = "Fathiah Nuraisyah Radam",
                NIM = "221",
                isActive = true
            };

            // Act
            var result = await repo.UpdateMahasiswaByID(invalidID, updatedMahasiswa);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task UpdateMahasiswaByID_NullMahasiswaData_Returns0()
        {
            // Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithEmptyContext();
            var validID = 1;
            MahasiswaData nullData = null;

            var result = await repo.UpdateMahasiswaByID(validID, nullData);
            Assert.Equal(0, result);

        }

        #endregion

        #region Add Mahasiswa
        [Fact]
        public async Task AddMahasiswa_ValidData_Returns1_AndDataIsAdded()
        {
            // Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithEmptyContext();

            var newMahasiswa = new MahasiswaData()
            {
                Name = "Fathiah Nuraisyah Rdm",
                NIM = "123456789",
                isActive = true
            };

            // Act
            var result = await repo.AddMahasiswa(newMahasiswa);

            // Assert
            Assert.Equal(1, result);

            var addedData = await repo.BrowseMahasiswaByNIM("123456789");
            Assert.NotNull(addedData);
            Assert.Equal("Fathiah Nuraisyah Rdm", addedData.Name);
            Assert.Equal("123456789", addedData.NIM);
            Assert.True(addedData.isActive);
        }

        [Fact]
        public async Task AddMahasiswa_NullData_Returns0()
        {
            // Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithEmptyContext();
            MahasiswaData nullData = null;

            // Act
            var result = await repo.AddMahasiswa(nullData);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task AddMahasiswa_DuplicateNIM_Returns0_AndDoesNotAdd()
        {
            // Arrange
            var repo = InMemoryDbHelper.GetRepositoryWithEmptyContext();

            var existingMahasiswa = new MahasiswaData()
            {
                Name = "Fathiah Nuraisyah Radam",
                NIM = "2210817120013",
                isActive = true
            };

            await repo.AddMahasiswa(existingMahasiswa);

            var newMahasiswaWithDuplicateNIM = new MahasiswaData()
            {
                Name = "Fathiah RDM",
                NIM = "2210817120013",  
                isActive = true
            };

            // Act
            var result = await repo.AddMahasiswa(newMahasiswaWithDuplicateNIM);

            // Assert
            Assert.Equal(0, result);  
            var data = await repo.BrowseMahasiswaByNIM("2210817120013");
            Assert.NotNull(data);
            Assert.Equal("Fathiah Nuraisyah Radam", data.Name);
        }
        #endregion

    }
}

