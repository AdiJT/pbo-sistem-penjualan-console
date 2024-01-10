using UtsPboAdi2206080051.Entitas.EntitasBarang;
using UtsPboAdi2206080051.EntitasSatuan;
using UtsPboAdi2206080051.JoinEntitas.Commons;

namespace UtsPboAdi2206080051.JoinEntitas.EntitasDetailBarang
{
    public class DetailBarang : BaseJoinEntitas<Barang, Satuan>
    {
        public DetailBarang() : base()
        {
            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(DetailBarang),
                NamaProperti = nameof(NamaBarang),
                NamaKolom = "Nama Barang",
                PanjangKolom = 25,
                FormatString = ""
            });

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(DetailBarang),
                NamaProperti = nameof(NamaSatuan),
                NamaKolom = "Satuan",
                PanjangKolom = 20,
                FormatString = ""
            });

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(DetailBarang),
                NamaProperti = nameof(HargaBarang),
                NamaKolom = "Harga Barang",
                PanjangKolom = 16,
                FormatString = "{0:C2}"
            });

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(DetailBarang),
                NamaProperti = nameof(StokBarang),
                NamaKolom = "Stok",
                PanjangKolom = 6,
                FormatString = ""
            });
        }

        public string NamaBarang { get => Entitas1.NamaBarang; }
        public string NamaSatuan { get => Entitas2.NamaSatuan; }

        public decimal HargaBarang { get; set; }
        public int StokBarang { get; set; }
    }
}
