using Mahasiswa.Domain.Entities;


namespace Mahasiswa.Application.Services
{
    public interface IMahasiswaServices
    {
        Task<List<MahasiswaData>> ReadMahasiswa();
        Task<MahasiswaData> BrowseMahasiswaByNIM(string NIM);
        Task<int> UpdateMahasiswaByID(int id, MahasiswaData mahasiswaData);
        Task<int> DeleteMahasiswaByID(int id);
        Task<int> AddMahasiswa(MahasiswaData mahasiswaData);

    }
}
