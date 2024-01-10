using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UtsPboAdi2206080051.Entitas.EntitasBarang;
using UtsPboAdi2206080051.Entitas.EntitasTransaksi;
using UtsPboAdi2206080051.EntitasSatuan;
using UtsPboAdi2206080051.JoinEntitas.Commons;

namespace UtsPboAdi2206080051.JoinEntitas.EntitasDetailTransaksi
{
    public class DetailTransaksi : BaseJoinEntitas<Transaksi, Barang>
    {
        public DetailTransaksi() : base()
        {
            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(DetailTransaksi),
                NamaProperti = nameof(NamaBarang),
                NamaKolom = "Nama Barang",
                PanjangKolom = 25,
                FormatString = ""
            });

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(DetailTransaksi),
                NamaProperti = nameof(HargaBarang),
                NamaKolom = "Harga Barang",
                PanjangKolom = 15,
                FormatString = "{0:C2}"
            });

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(DetailTransaksi),
                NamaProperti = nameof(Satuan),
                NamaKolom = "Satuan",
                PanjangKolom = 25,
                FormatString = ""
            });

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(DetailTransaksi),
                NamaProperti = nameof(Jumlah),
                NamaKolom = "Jumlah Barang",
                PanjangKolom = 5,
                FormatString = ""
            });

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(DetailTransaksi),
                NamaProperti = nameof(SubTotal),
                NamaKolom = "Sub Total",
                PanjangKolom = 15,
                FormatString = "{0:C2}"
            });
        }


        public string NamaBarang { get; set; }
        public decimal HargaBarang { get; set; }

        public string Satuan { get; set; }
        public int Jumlah { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey(nameof(EntitasSatuan))]
        public string IdSatuan { get; set; }

        public virtual Satuan EntitasSatuan { get; set; }

        [NotMapped]
        public decimal SubTotal { get => HargaBarang * Jumlah; }
    }
}
