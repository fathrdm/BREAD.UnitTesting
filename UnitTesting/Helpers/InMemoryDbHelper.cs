using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahasiswa.Domain.Entities;
using Mahasiswa.Domain.Interface;
using Mahasiswa.Infrastructure.Data;
using Mahasiswa.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTesting.Helpers
{
    public class InMemoryDbHelper
    {
        public static MahasiswaDBContext GetContext()
        {
            var options = new DbContextOptionsBuilder<MahasiswaDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new MahasiswaDBContext(options);
            context.Database.EnsureCreated();
            return context;
        }
        public static MahasiswaDBContext GetSeededContext()
        {
            var context = GetContext();

            context.MahasiswaData.AddRange(new MahasiswaData
            {
                Id = 1,
                Name = "Fathiah Nuraisyah Radam",
                NIM = "2210817120013",
                isActive = true
            },
                 new MahasiswaData
                 {
                     Id = 2,
                     Name = "Muhammad Fulan",
                     NIM = "2210817120009",
                     isActive = true
                 }
            );

            context.SaveChanges();
            return context;
        }
        public static MahasiswaRepository GetRepositoryWithSeededContext()
        {
            var context = InMemoryDbHelper.GetSeededContext();
            return new MahasiswaRepository(context);
        }
        public static MahasiswaRepository GetRepositoryWithEmptyContext()
        {
            // Setup in-memory db context tanpa data
            var options = new DbContextOptionsBuilder<MahasiswaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new MahasiswaDBContext(options);

            return new MahasiswaRepository(context);
        }

    }
}
