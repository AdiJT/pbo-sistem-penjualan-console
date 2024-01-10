using System.Linq;
using System;
using UtsPboAdi2206080051.Entitas.Commons;
using UtsPboAdi2206080051.Entitas.EntitasBarang;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailTransaksi;
using System.Collections.Generic;
using UtsPboAdi2206080051.Entitas.EntitasSatuan;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailBarang;
using System.ComponentModel.Design;

namespace UtsPboAdi2206080051.Entitas.EntitasTransaksi
{
    public class RepositoriTransaksi : IRepositori<Transaksi>
    {
        public Transaksi InputCreate(AppDbContext db)
        {
            var repositoriBarang = new RepositoriBarang();

            if (repositoriBarang.GetList(db).Count == 0)
                throw new Exception("Daftar Barang kosong");

            string id = Guid.NewGuid().ToString();
            DateTime tanggal = DateTime.Now;
            decimal diskon;

            var transaksi = new Transaksi()
            {
                Id = id,
                Tanggal = tanggal,
                DaftarDetailTransaksi = new List<DetailTransaksi>()
            };            

            diskon = Utilitas.InputDecimal("Diskon (dalam persen)", (d) => d >= 0, "Diskon kurang dari 0");

            transaksi.Diskon = diskon;

            return transaksi;
        }

        public Transaksi InputUpdate(AppDbContext db)
        {
            DateTime tanggal;
            decimal diskon;

            CetakTabel(db);
            var listTransaksi = GetList(db);
            var index = Utilitas.InputInt("Nomor di atas", i => i > 0 && i <= listTransaksi.Count,
                () => throw new Exception("Proses Berhenti"), 
                "Transaksi dengan nomor {0} tidak ada");
            var transaksi = listTransaksi[index - 1];

            Console.WriteLine("Masukan Data Baru");

            tanggal = Utilitas.InputDate("Tanggal", null);
            diskon = Utilitas.InputDecimal("Diskon (dalam persen)", (d) => d >= 0, "Diskon kurang dari 0");

            transaksi.Tanggal = tanggal;
            transaksi.Diskon = diskon;

            return transaksi;
        }

        public Transaksi InputDelete(AppDbContext db)
        {
            string id;

            CetakTabel(db);
            var listTransaksi = GetList(db);
            var index = Utilitas.InputInt("Nomor di atas", i => i > 0 && i <=listTransaksi.Count(), 
                () => throw new Exception("Proses Berhenti"), 
                "Transaksi dengan nomor {0} tidak ada");
            var transaksi = listTransaksi[index - 1];
            id = transaksi.Id;

            return Get(id, db);
        }

        public void Add(Transaksi Entitas, AppDbContext db)
        {
            try
            {
                db.TblTransaksi.Add(Entitas);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Transaksi Entitas, AppDbContext db)
        {
            try
            {
                var transaksi = Get(Entitas.Id, db);

                transaksi.Tanggal = Entitas.Tanggal;
                transaksi.Diskon = Entitas.Diskon;
                transaksi.DaftarDetailTransaksi = Entitas.DaftarDetailTransaksi;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(Transaksi entitas, AppDbContext db)
        {
            try
            {
                if (entitas.DaftarDetailTransaksi != null)
                    Console.WriteLine($"{entitas.DaftarDetailTransaksi.Count} detail transaksi ikut tehapus");
                
                db.TblTransaksi.Remove(entitas);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CetakTabel(AppDbContext db, Func<Transaksi, bool> kondisi = null)
        {
            try
            {
                var listTransaksi = GetList(db);
                var listKolom = new Transaksi().ListKolom;

                if (kondisi != null)
                    listTransaksi = listTransaksi.Where(b => kondisi(b)).ToList();

                Console.Write(Utilitas.BuatTabel(listTransaksi, listKolom, true));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CetakNota(Transaksi transaksi, AppDbContext db)
        {
            try
            {
                var repositoriDetailTransaksi = new RepositoriDetailTransaksi();

                Console.WriteLine();
                Console.WriteLine(Utilitas.BuatJudul("Nota Pembelian"));
                Console.WriteLine();
                Console.WriteLine($"No Nota : {transaksi.Id}");
                Console.WriteLine($"Tanggal : {transaksi.Tanggal:d}");

                Console.WriteLine();

                repositoriDetailTransaksi.CetakTabel(transaksi, db);
                Console.WriteLine($"{"Total", -12} : {transaksi.Total:C2}");
                Console.WriteLine($"{"Diskon", -12} : {transaksi.Diskon}%");
                Console.WriteLine($"{"Total Bayar", -12} : {transaksi.TotalBayar:C2}");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Transaksi TambahDetailTransaksi(Transaksi transaksi, AppDbContext db)
        {
            var repositoriBarang = new RepositoriBarang();
            var repositoriDetailBarang = new RepositoriDetailBarang();
            var repositoriDetailTransaksi = new RepositoriDetailTransaksi();
            while (transaksi.DaftarDetailTransaksi.Count < repositoriDetailBarang.GetList(db).Count)
            {
                try
                {
                    repositoriBarang.CetakTabel(db, (b) => b.DaftarDetailBarang.Sum(bs => bs.StokBarang) > 0);
                    var idBarang = Utilitas.InputString("ID Barang", 
                        s => repositoriBarang.IsExist(s, db), 
                        null, "Barang tidak ada");
                    var barang = repositoriBarang.Get(idBarang, db);

                    var detailTransaksi = repositoriDetailTransaksi.InputCreate(transaksi, barang, db);

                    transaksi.DaftarDetailTransaksi.Add(detailTransaksi);

                    Console.Write("Tambah Barang [y/n] : ");
                    var pilih = Console.ReadLine().Trim().ToLower();
                    if (pilih != "y")
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }

            return transaksi;
        }

        public List<Transaksi> GetList(AppDbContext db)
        {
            try
            {
                var listTransaksi = db.TblTransaksi.ToList();
                return listTransaksi;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Transaksi Get(string id, AppDbContext db)
        {
            try
            {
                return db.TblTransaksi.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsExist(string id, AppDbContext db)
        {
            try
            {
                return Get(id, db) != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int JumlahEntitas(AppDbContext db, Func<Transaksi, bool> kondisi = null)
        {
            try
            {
                var list = GetList(db);
                if (kondisi != null)
                    list = list.Where(e => kondisi(e)).ToList();
                return list.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
