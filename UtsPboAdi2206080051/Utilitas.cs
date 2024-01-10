using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtsPboAdi2206080051.Entitas.Commons;

namespace UtsPboAdi2206080051
{
    public class Utilitas
    {
        public static readonly string CancelString = "\\\\Cancel\\\\";

        //Method input, validasi menghasilkan nilai true jika lulus validasi
        public static string InputString(string namaVariabel, Func<string, bool> validasi , Action onValidasiGagal, string pesanValidasi = "")
        {
            string input = "";
            while (true)
            {
                Console.Write($"Masukan {namaVariabel} (Masukan {CancelString} untuk berhenti): ");
                input = Console.ReadLine().Trim();

                //Cek berhenti input
                if (input == CancelString)
                    throw new Exception("Proses Input Dihentikan");

                //Validasi
                if (input == "")
                    Console.WriteLine($"{namaVariabel} belum diisi");
                else if (validasi != null && !validasi(input))
                {
                    Console.WriteLine(string.Format(pesanValidasi, input));
                    if (onValidasiGagal != null)
                        onValidasiGagal();
                }
                else
                    break;
            }
            return input;
        }

        public static decimal InputDecimal(string namaVariabel, Func<decimal, bool> validasi, string pesanValidasi = "")
        {
            decimal input = 0;
            while (true)
            {
                Console.Write($"Masukan {namaVariabel} (Masukan {CancelString} untuk berhenti): ");
                var strInput = Console.ReadLine().Trim();

                //Cek berhenti input
                if (strInput == CancelString)
                    throw new Exception("Proses Input Dihentikan");

                //Validasi
                if (strInput == "")
                    Console.WriteLine($"{namaVariabel} belum diisi");
                else if (!decimal.TryParse(strInput, out input))
                    Console.WriteLine($"Format {namaVariabel} salah");
                else if (validasi != null && !validasi(input))
                    Console.WriteLine(string.Format(pesanValidasi, input));
                else
                    break;
            }
            return input;
        }

        public static int InputInt(string namaVariabel, Func<int, bool> validasi, Action onValidasiGagal, string pesanValidasi = "")
        {
            int input = 0;
            while (true)
            {
                Console.Write($"Masukan {namaVariabel} (Masukan {CancelString} untuk berhenti): ");
                var strInput = Console.ReadLine().Trim();

                //Cek berhenti input
                if (strInput == CancelString)
                    throw new Exception("Proses Input Dihentikan");

                //Validasi
                if (strInput == "")
                    Console.WriteLine($"{namaVariabel} belum diisi");
                else if (!int.TryParse(strInput, out input))
                    Console.WriteLine($"Format {namaVariabel} salah");
                else if (validasi != null && !validasi(input))
                    Console.WriteLine(string.Format(pesanValidasi, input));
                else
                    break;
            }
            return input;
        }

        public static DateTime InputDate(string namaVariabel, Func<DateTime, bool> validasi, string pesanValidasi = "")
        {
            DateTime input = new DateTime();
            while (true)
            {
                Console.Write($"Masukan {namaVariabel} (Masukan {CancelString} untuk berhenti): ");
                var strInput = Console.ReadLine().Trim();

                //Cek berhenti input
                if (strInput == CancelString)
                    throw new Exception("Proses Input Dihentikan");

                //Validasi
                if (strInput == "")
                    Console.WriteLine($"{namaVariabel} belum diisi");
                else if (!DateTime.TryParse(strInput, out input))
                    Console.WriteLine($"Format {namaVariabel} salah");
                else if (validasi != null && !validasi(input))
                    Console.WriteLine(string.Format(pesanValidasi, input));
                else
                    break;
            }
            return input;
        }

        public static string BuatJudul(string judul)
        {
            var strJudul = "";
            var strBorder = "";

            //Buat Border
            strBorder += "+";
            for (int i = 0; i < judul.Length; i++)
                strBorder += "-";
            strBorder += "+";

            //Buat Judul
            strJudul += strBorder;
            strJudul += $"\n|{judul}|\n";
            strJudul += strBorder;

            return strJudul;
        }

        public static string BuatTabel<T>(List<T> listEntitas, List<Kolom> listKolom, bool useNumbering = false)
        {
            if (useNumbering == true)
                listKolom.Insert(0, new Kolom()
                {
                    NamaEntitas = nameof(Int32),
                    NamaProperti = nameof(Int32),
                    NamaKolom = "Nomor",
                    PanjangKolom = 5,
                    FormatString = ""
                });

            var strTabel = "";

            //Mengambil panjang untuk masing2 kolom maksimal dari kolom.PanjangKolom, panjang nama kolom,
            //dan panjang cells terbesar
            List<int> listPanjangKolom = new List<int>();
            foreach(var item in listKolom)
            {
                int panjang = Math.Max(item.NamaKolom.Length + 2, item.PanjangKolom); // + 2 untuk spasi
                if (item.NamaKolom == "Nomor")
                    panjang = Math.Max(panjang, 5);
                else
                {
                    var maxPanjang = 0;
                    if(listEntitas.Count > 0)
                        maxPanjang = (from e in listEntitas
                                     select e.GetType().GetProperty(item.NamaProperti).GetValue(e).ToString().Length).Max();
                    panjang = Math.Max(panjang, maxPanjang + 2);
                }
                listPanjangKolom.Add(panjang);
            }

            //Buat Border
            string border = "";
            foreach(var item in listPanjangKolom)
            {
                border += "+";
                for(int i = 0; i < item + 1; i++)
                    border += "-";
            }
            border += "+\n";

            //Buat Header
            strTabel += border;
            for (int i = 0; i < listKolom.Count; i++)
            {
                strTabel += $"| {listKolom[i].NamaKolom}";
                if (listKolom[i].NamaKolom.Length < listPanjangKolom[i])
                    for (int j = 0; j < listPanjangKolom[i] - listKolom[i].NamaKolom.Length; j++)
                        strTabel += " ";
            }
            strTabel += "|\n";
            strTabel += border;

            //Membuat isi
            for (int k = 0; k < listEntitas.Count(); k++)
            {
                for (int i = 0; i < listKolom.Count; i++)
                {
                    strTabel += "| ";
                    if(i == 0 && useNumbering == true)
                    {
                        var strNomor = (k + 1).ToString();

                        if (strNomor.Length > listPanjangKolom[i])
                            strNomor = strNomor.Remove(listPanjangKolom[i] - 2) + "..";

                        strTabel += strNomor;

                        if (strNomor.Length < listPanjangKolom[i])
                            for (int j = 0; j < listPanjangKolom[i] - strNomor.Length; j++)
                                strTabel += " ";

                        continue;
                    }


                    string strIsi = "";
                    if (listKolom[i].FormatString.Length > 0)
                        strIsi = string.Format(listKolom[i].FormatString, listEntitas[k].GetType().GetProperty(listKolom[i].NamaProperti).GetValue(listEntitas[k]));
                    else
                        strIsi = listEntitas[k].GetType().GetProperty(listKolom[i].NamaProperti).GetValue(listEntitas[k]).ToString();

                    if (strIsi.Length > listPanjangKolom[i])
                        strIsi = strIsi.Remove(listPanjangKolom[i] - 3) + "..";

                    strTabel += strIsi;

                    if (strIsi.Length < listPanjangKolom[i])
                        for (int j = 0; j < listPanjangKolom[i] - strIsi.Length; j++)
                            strTabel += " ";
                }
                strTabel += "|\n";
                strTabel += border;
            }

            return strTabel;
        }
    }
}
