using System;
using System.Linq;
using UtsPboAdi2206080051.Entitas.EntitasBarang;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailBarang;
using UtsPboAdi2206080051.Menu.Commons;

namespace UtsPboAdi2206080051.Menu
{
    public class MenuBarang : BaseMenu<Barang>
    {
        public MenuBarang() : base(new RepositoriBarang(), nameof(Barang), new AppDbContext())
        {
            
        }

        public override void MenuTambah()
        {
            Console.WriteLine();
            Console.WriteLine(Utilitas.BuatJudul($"Tambah {namaEntitas}"));

            try
            {
                var barangBaru = iRepositori.InputCreate(db);
                barangBaru = (iRepositori as RepositoriBarang).TambahSatuan(barangBaru, db);
                iRepositori.Add(barangBaru, db);
                Console.WriteLine($"{namaEntitas} dengan ID '{barangBaru.Id}' berhasil ditambahkan");
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
                Barang barang = new Barang();
                var repositoriDetailBarang = new RepositoriDetailBarang();

                Console.WriteLine("Menu Ubah");
                Console.WriteLine("1. Ubah Barang");
                Console.WriteLine("2. Tambah Satuan untuk Barang");
                Console.WriteLine("3. Ubah Satuan dari barang");
                Console.WriteLine("4. Kembali");
                Console.Write("Masukan Pilihan : ");
                var pilih = Console.ReadLine().Trim();

                switch (pilih)
                {
                    case "1":
                        barang = iRepositori.InputUpdate(db);
                        iRepositori.Update(barang, db);
                        break;

                    case "2":
                        {
                            var id = Utilitas.InputString("ID Barang",
                                    s => iRepositori.IsExist(s, db),
                                    () => throw new Exception("Proses input berhenti"),
                                    "Barang dengan ID '{0}' tidak ada"
                            );

                            barang = iRepositori.Get(id, db);

                            barang = (iRepositori as RepositoriBarang).TambahSatuan(barang, db);
                            iRepositori.Update(barang, db);
                            break;
                        }
                    case "3":
                        {
                            var id = Utilitas.InputString("ID Barang",
                                    s => iRepositori.IsExist(s, db),
                                    () => throw new Exception("Proses input berhenti"),
                                    "Barang dengan ID '{0}' tidak ada"
                            );

                            barang = iRepositori.Get(id, db);

                            repositoriDetailBarang.CetakTabel(barang, db);
                            var listDetailBarang = repositoriDetailBarang.GetList(db).Where(dt => dt.IdEntitas1 == barang.Id).ToList();
                            var index = Utilitas.InputInt("Nomor di atas",
                                (i) => i > 0 && i <= listDetailBarang.Count(),
                                () => throw new Exception("Proses Berhenti"),
                                "Satuan Barang dengan nomor {0} tidak ada");
                            var DetailBarang = listDetailBarang[index - 1];

                            DetailBarang = repositoriDetailBarang.InputUpdate(DetailBarang, db);
                            repositoriDetailBarang.Update(DetailBarang, db);

                            Console.WriteLine($"Satuan Barang berhasil diubah");
                            break;
                        }

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Pilihan tidak tersedia. Silahkan masukan ulang");
                        break;
                }

                Console.WriteLine($"{namaEntitas} dengan ID '{barang.Id}' berhasil diubah");
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
