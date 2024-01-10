using System.Collections.Generic;
using UtsPboAdi2206080051.Entitas.Commons;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailBarang;

namespace UtsPboAdi2206080051.EntitasSatuan
{
    public class Satuan : BaseEntitas
    {
        public Satuan() : base()
        {
            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(Satuan),
                NamaProperti = nameof(NamaSatuan),
                NamaKolom = "Nama Satuan",
                PanjangKolom = 25,
                FormatString = ""
            });
        }

        public string NamaSatuan { get; set; }

        public virtual ICollection<DetailBarang> DaftarDetailBarang { get; set; }
    }
}
