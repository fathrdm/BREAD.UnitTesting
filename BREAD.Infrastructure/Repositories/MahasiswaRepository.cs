using System;
using System.Collections.Generic;
using System.Linq;
using Mahasiswa.Domain.Entities;
using Mahasiswa.Domain.Interface;
using Mahasiswa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Mahasiswa.Infrastructure.Repositories
{
    public class MahasiswaRepository : IMahasiswaRepository
    {
        private readonly MahasiswaDBContext _mahasiswaDBContext;
        public MahasiswaRepository(MahasiswaDBContext mahasiswaDBContext)
        {
            _mahasiswaDBContext = mahasiswaDBContext;
        }
        public async Task<List<MahasiswaData>> ReadMahasiswa()
        {
            return await _mahasiswaDBContext.MahasiswaData.ToListAsync();
        }

        public async Task<MahasiswaData> BrowseMahasiswaByNIM(string NIM)
        {
            return await _mahasiswaDBContext.MahasiswaData
                          .FirstOrDefaultAsync(b => b.NIM == NIM);
        }

        public async Task<int> UpdateMahasiswaByID(int id, MahasiswaData mahasiswaData)
        {
            var entity = await _mahasiswaDBContext.MahasiswaData.FindAsync(id);
            if (entity == null) return 0;

            entity.Name = mahasiswaData.Name;
            entity.NIM = mahasiswaData.NIM;
            entity.isActive = mahasiswaData.isActive;

            _mahasiswaDBContext.MahasiswaData.Update(entity);
            await _mahasiswaDBContext.SaveChangesAsync();

            return 1;
        }

        public async Task<int> DeleteMahasiswaByID(int id)
        {
            var entity = await _mahasiswaDBContext.MahasiswaData.FindAsync(id);
            if (entity == null) return 0;

            _mahasiswaDBContext.MahasiswaData.Remove(entity);
            await _mahasiswaDBContext.SaveChangesAsync();

            return 1;
        }

        public async Task<int> AddMahasiswa(MahasiswaData mahasiswaData)
        {
            if (mahasiswaData == null) return 0;
            await _mahasiswaDBContext.MahasiswaData.AddAsync(mahasiswaData);
            await _mahasiswaDBContext.SaveChangesAsync();
            return 1; 
        }
    }
}
