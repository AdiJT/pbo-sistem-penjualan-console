using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using UtsPboAdi2206080051.Entitas.Commons;
using UtsPboAdi2206080051.Entitas.EntitasKategori;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailBarang;

namespace UtsPboAdi2206080051.Entitas.EntitasBarang
{
    //Relasi : 
    //Barang - Kategori : Many to One
    //Barang - Satuan : Many to Many , join entity DetailBarang
    //Barang - Transaksi : Many to Many , join entity DetailTransaksi
    public class Barang : BaseEntitas
    {
        public Barang() : base()
        {
            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(Barang),
                NamaProperti = nameof(NamaBarang),
                NamaKolom = "Nama Barang",
                PanjangKolom = 25,
                FormatString = ""
            });

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(Barang),
                NamaProperti = nameof(StrdaftarSatuan),
                NamaKolom = "(Satuan | Harga | Stok)",
                PanjangKolom = 30,
                FormatString = ""
            });

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(Barang),
                NamaProperti = nameof(IdKategori),
                NamaKolom = "Id Kategori",
                PanjangKolom = 10,
                FormatString = ""
            });
        }

        public string NamaBarang { get; set; }

        [ForeignKey(nameof(Kategori))]
        public string IdKategori { get; set; }

        public string StrdaftarSatuan { get => string.Join(", ",
                DaftarDetailBarang.Select(bs => $"({bs.NamaSatuan} | {bs.HargaBarang:C2} | {bs.StokBarang})")
            );
        }

        public virtual Kategori Kategori { get; set; }
        public virtual ICollection<DetailBarang> DaftarDetailBarang { get; set; }
    }
}
