using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtsPboAdi2206080051.Entitas;
using UtsPboAdi2206080051.Menu;

namespace UtsPboAdi2206080051
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(Utilitas.BuatJudul("Sistem Penjualan"));
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(Utilitas.BuatJudul("Menu Utama"));
                Console.WriteLine("Nama : Adi Juanito Taklal");
                Console.WriteLine("NIM : 2206080051");
                Console.WriteLine("1. Barang");
                Console.WriteLine("2. Kategori");
                Console.WriteLine("3. Satuan");
                Console.WriteLine("4. Transaksi");
                Console.WriteLine("5. Keluar");

                Console.Write("Masukan Pilihan : ");
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        new MenuBarang().MainMenu();
                        break;

                    case "2":
                        new MenuKategori().MainMenu();
                        break;

                    case "3":
                        new MenuSatuan().MainMenu();
                        break;

                    case "4":
                        new MenuTransaksi().MainMenu();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Masukan salah. Pilih pilihan yang tersedia");
                        break;
                }
            }
        }
    }
}
