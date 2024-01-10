using System.Data.Entity;
using UtsPboAdi2206080051.Entitas.EntitasBarang;
using UtsPboAdi2206080051.Entitas.EntitasKategori;
using UtsPboAdi2206080051.Entitas.EntitasTransaksi;
using UtsPboAdi2206080051.EntitasSatuan;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailBarang;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailTransaksi;

namespace UtsPboAdi2206080051
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=DefaultConnection")
        {
            
        }

        public DbSet<Barang> TblBarang { get; set; }
        public DbSet<Kategori> TblKategori { get; set; }
        public DbSet<Satuan> TblSatuan { get; set; }
        public DbSet<Transaksi> TblTransaksi { get; set; }
        public DbSet<DetailTransaksi> TblDetailTransaksi { get; set; }
        public DbSet<DetailBarang> TblDetailBarang { get; set; }
    }
}
