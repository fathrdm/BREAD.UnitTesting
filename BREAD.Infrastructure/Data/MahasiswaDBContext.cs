using Mahasiswa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mahasiswa.Infrastructure.Data
{
    public class MahasiswaDBContext : DbContext
    {
        public MahasiswaDBContext(DbContextOptions<MahasiswaDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<MahasiswaData> MahasiswaData { get; set; }
    }
}
