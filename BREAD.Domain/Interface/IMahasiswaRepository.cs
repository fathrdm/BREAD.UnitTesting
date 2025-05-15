using Mahasiswa.Domain.Entities;

namespace Mahasiswa.Domain.Interface
{
    public interface IMahasiswaRepository
    {
        Task<List<MahasiswaData>> ReadMahasiswa();
        Task<int> UpdateMahasiswaByID (int id, MahasiswaData mahasiswaData);
        Task<int> AddMahasiswa(MahasiswaData mahasiswaData);
        Task<int> DeleteMahasiswaByID (int id);
        Task<MahasiswaData> BrowseMahasiswaByNIM(string NIM);
    }
}
