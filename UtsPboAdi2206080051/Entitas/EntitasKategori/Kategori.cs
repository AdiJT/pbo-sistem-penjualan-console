using System.Collections.Generic;
using UtsPboAdi2206080051.Entitas.Commons;
using UtsPboAdi2206080051.Entitas.EntitasBarang;

namespace UtsPboAdi2206080051.Entitas.EntitasKategori
{
    public class Kategori : BaseEntitas
    {
        public Kategori() : base()
        {
            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(Kategori),
                NamaProperti = nameof(NamaKategori),
                NamaKolom = "Nama Kategori",
                PanjangKolom = 25,
                FormatString = ""
            });
        }

        public string NamaKategori { get; set; }

        public virtual ICollection<Barang> DaftarBarang { get; set; }
    }
}
