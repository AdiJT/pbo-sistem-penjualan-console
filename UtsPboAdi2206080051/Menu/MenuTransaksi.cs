using System;
using System.Linq;
using UtsPboAdi2206080051.Entitas.EntitasTransaksi;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailTransaksi;
using UtsPboAdi2206080051.Menu.Commons;

namespace UtsPboAdi2206080051.Menu
{
    public class MenuTransaksi : BaseMenu<Transaksi>
    {
        public MenuTransaksi() : base(new RepositoriTransaksi(), nameof(Transaksi), new AppDbContext())
        {

        }

        public override void MenuDaftar()
        {
            base.MenuDaftar();

            try
            {
                if (iRepositori.JumlahEntitas(db) == 0)
                    return;

                Console.Write("Cetak Nota[y/n]? : ");
                var pilih = Console.ReadLine().Trim().ToLower();
                if (pilih == "y")
                {
                    var listTransaksi = iRepositori.GetList(db);
                    var index = Utilitas.InputInt("Nomor Transaksi di atas",
                        (i) => i > 0 && i <= listTransaksi.Count,
                        () => throw new Exception("Proses Berhenti"),
                        "Transaksi dengan nomor {0} tidak ada");
                    var transaksi = listTransaksi[index - 1];

                    (iRepositori as RepositoriTransaksi).CetakNota(transaksi, db);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Tekan tombol apapun untuk melanjutkan");
                Console.ReadKey(true);
            }
        }

        public override void MenuTambah()
        {
            Console.WriteLine();
            Console.WriteLine(Utilitas.BuatJudul($"Tambah {namaEntitas}"));

            try
            {
                var transaksiBaru = iRepositori.InputCreate(db);
                transaksiBaru = (iRepositori as RepositoriTransaksi).TambahDetailTransaksi(transaksiBaru, db);
                iRepositori.Add(transaksiBaru, db);

                Console.WriteLine($"{namaEntitas} dengan ID '{transaksiBaru.Id}' berhasil ditambahkan");

                Console.Write("Cetak Nota[y/n]? : ");
                var pilih = Console.ReadLine().Trim().ToLower();
                if (pilih == "y")
                    (iRepositori as RepositoriTransaksi).CetakNota(transaksiBaru, db);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Tekan tombol apapun untuk melanjutkan");
                Console.ReadKey(true);
            }
        }

        public override void MenuUbah()
        {
            Console.WriteLine();
            Console.WriteLine(Utilitas.BuatJudul($"Ubah {namaEntitas}"));

            try
            {
                Transaksi transaksi = new Transaksi();
                var repositoriDetailTransaksi = new RepositoriDetailTransaksi();

                Console.WriteLine("Menu Ubah");
                Console.WriteLine("1. Ubah Transaksi");
                Console.WriteLine("2. Tambah Detail Transaksi");
                Console.WriteLine("3. Ubah Detail Transaksi");
                Console.WriteLine("4. Kembali");
                Console.Write("Masukan Pilihan : ");
                var pilih = Console.ReadLine().Trim();

                switch(pilih)
                {
                    case "1":
                        transaksi = iRepositori.InputUpdate(db);
                        iRepositori.Update(transaksi, db);
                        break;

                    case "2":
                        {
                            iRepositori.CetakTabel(db);

                            var listTransaksi = iRepositori.GetList(db);
                            var index = Utilitas.InputInt("Nomor di atas", 
                                (i) => i > 0 && i <= listTransaksi.Count, 
                                () => throw new Exception("Proses Berhenti"), 
                                "Transaksi dengan nomor {0} tidak ada");

                            transaksi = listTransaksi[index - 1];

                            transaksi = (iRepositori as RepositoriTransaksi).TambahDetailTransaksi(transaksi, db);
                            iRepositori.Update(transaksi, db);
                            break;
                        }
                    case "3":
                        {
                            iRepositori.CetakTabel(db);
                            var listTransaksi = iRepositori.GetList(db);
                            var index = Utilitas.InputInt("Nomor di atas",
                                (i) => i > 0 && i <= listTransaksi.Count(),
                                () => throw new Exception("Proses Berhenti"),
                                "Transaksi dengan nomor {0} tidak ada");
                            transaksi = listTransaksi[index - 1];

                            repositoriDetailTransaksi.CetakTabel(transaksi, db);
                            var listDetailTransaksi = repositoriDetailTransaksi.GetList(db).Where(dt => dt.IdEntitas1 == transaksi.Id).ToList();
                            index = Utilitas.InputInt("Nomor di atas",
                                (i) => i > 0 && i <= listDetailTransaksi.Count(),
                                () => throw new Exception("Proses Berhenti"),
                                "Detail Transaksi dengan nomor {0} tidak ada");
                            var detailTransaksi = listDetailTransaksi[index - 1];

                            detailTransaksi = repositoriDetailTransaksi.InputUpdate(detailTransaksi, db);
                            repositoriDetailTransaksi.Update(detailTransaksi, db);

                            Console.WriteLine($"Detail Transaksi berhasil diubah");
                            break;
                        }

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Pilihan tidak tersedia. Silahkan masukan ulang");
                        break;
                }

                Console.WriteLine($"{namaEntitas} dengan ID '{transaksi.Id}' berhasil diubah");

                Console.Write("Cetak Nota[y/n]? : ");
                pilih = Console.ReadLine().Trim().ToLower();
                if (pilih == "y")
                    (iRepositori as RepositoriTransaksi).CetakNota(transaksi, db);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Tekan tombol apapun untuk melanjutkan");
                Console.ReadKey(true);
            }
        }
    }
}
