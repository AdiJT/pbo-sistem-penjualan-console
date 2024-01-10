using System;
using System.Linq;
using UtsPboAdi2206080051.Entitas.Commons;

namespace UtsPboAdi2206080051.Menu.Commons
{
    public abstract class BaseMenu<T> where T : BaseEntitas
    {
        protected IRepositori<T> iRepositori;
        protected readonly AppDbContext db;
        protected string namaEntitas;

        public BaseMenu(IRepositori<T> iRepositori, string namaEntitas, AppDbContext db)
        {
            this.iRepositori = iRepositori;
            this.namaEntitas = namaEntitas;
            this.db = db;
        }

        public virtual void MainMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(Utilitas.BuatJudul($"Menu {namaEntitas}"));
                Console.WriteLine($"1. Daftar {namaEntitas}");
                Console.WriteLine($"2. Tambah {namaEntitas}");
                Console.WriteLine($"3. Ubah {namaEntitas}");
                Console.WriteLine($"4. Hapus {namaEntitas}");
                Console.WriteLine("5. Kembali");
                Console.Write("Masukan Pilihan : ");
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        MenuDaftar();
                        break;

                    case "2":
                        MenuTambah();
                        break;

                    case "3":
                        MenuUbah();
                        break;

                    case "4":
                        MenuHapus();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Masukan salah. Pilih pilihan yang tersedia");
                        break;
                }
            }
        }

        public virtual void MenuDaftar()
        {
            Console.WriteLine();
            Console.WriteLine(Utilitas.BuatJudul($"Daftar {namaEntitas}"));

            if (iRepositori.GetList(db).Count() == 0)
            {
                Console.WriteLine($"Daftar {namaEntitas} masih kosong....");
                return;
            }

            try
            {
                iRepositori.CetakTabel(db);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Tekan tombol apapun untuk melanjutkan");
                Console.ReadKey(true);
            }
        }

        public virtual void MenuTambah()
        {
            Console.WriteLine();
            Console.WriteLine(Utilitas.BuatJudul($"Tambah {namaEntitas}"));

            try
            {
                var entitasBaru = iRepositori.InputCreate(db);
                iRepositori.Add(entitasBaru, db);
                Console.WriteLine($"{namaEntitas} dengan ID '{entitasBaru.Id}' berhasil ditambahkan");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Tekan tombol apapun untuk melanjutkan");
                Console.ReadKey(true);
            }
        }

        public virtual void MenuUbah()
        {
            Console.WriteLine();
            Console.WriteLine(Utilitas.BuatJudul($"Ubah {namaEntitas}"));

            try
            {
                var entitas = iRepositori.InputUpdate(db);
                iRepositori.Update(entitas, db);
                Console.WriteLine($"{namaEntitas} dengan ID '{entitas.Id}' berhasil diubah");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Tekan tombol apapun untuk melanjutkan");
                Console.ReadKey(true);
            }
        }

        public virtual void MenuHapus()
        {
            Console.WriteLine();
            Console.WriteLine(Utilitas.BuatJudul($"Hapus {namaEntitas}"));

            try
            {
                var idEntitas = iRepositori.InputDelete(db);
                iRepositori.Delete(idEntitas, db);
                Console.WriteLine($"{namaEntitas} dengan ID '{idEntitas.Id}' berhasil dihapus");
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
