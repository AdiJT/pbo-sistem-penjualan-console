using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using UtsPboAdi2206080051.Entitas.Commons;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailTransaksi;

namespace UtsPboAdi2206080051.Entitas.EntitasTransaksi
{
    public class Transaksi : BaseEntitas
    {
        public Transaksi() : base()
        {
            ListKolom.RemoveAll(k => true);

            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(Transaksi),
                NamaProperti = nameof(Id),
                NamaKolom = nameof(Id),
                PanjangKolom = 38,
                FormatString = ""
            });

            ListKolom.Add(new Kolom()
            {
                NamaEntitas = nameof(Transaksi),
                NamaProperti = nameof(Tanggal),
                NamaKolom = "Tanggal",
                PanjangKolom = 20,
                FormatString = "{0:d}"
            });

            ListKolom.Add(new Kolom()
            {
                NamaEntitas = nameof(Transaksi),
                NamaProperti = nameof(Total),
                NamaKolom = "Total",
                PanjangKolom = 17,
                FormatString = "{0:C2}"
            });

            ListKolom.Add(new Kolom()
            {
                NamaEntitas = nameof(Transaksi),
                NamaProperti = nameof(Diskon),
                NamaKolom = "Diskon",
                PanjangKolom = 6,
                FormatString = ""
            });

            ListKolom.Add(new Kolom()
            {
                NamaEntitas = nameof(Transaksi),
                NamaProperti = nameof(TotalBayar),
                NamaKolom = "Total Bayar",
                PanjangKolom = 17,
                FormatString = "{0:C2}"
            });
        }

        public DateTime Tanggal { get; set; }
        public decimal Total { get => DaftarDetailTransaksi.Select(dt => dt.SubTotal).Sum(); }
        public decimal Diskon { get; set; }

        public virtual ICollection<DetailTransaksi> DaftarDetailTransaksi { get; set; }

        [NotMapped]
        public decimal TotalBayar { get => Total - Total * (Diskon / 100); }
    }
}
