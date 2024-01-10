using System;
using System.Collections.Generic;
using System.Linq;
using UtsPboAdi2206080051.Entitas.EntitasBarang;
using UtsPboAdi2206080051.Entitas.EntitasTransaksi;
using UtsPboAdi2206080051.JoinEntitas.Commons;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailBarang;

namespace UtsPboAdi2206080051.JoinEntitas.EntitasDetailTransaksi
{
    public class RepositoriDetailTransaksi : IJoinRepositori<Transaksi, Barang, DetailTransaksi>
    {
        public DetailTransaksi InputCreate(Transaksi Entitas1, Barang Entitas2, AppDbContext db)
        {
            var repositoriDetailBarang = new RepositoriDetailBarang();

            Console.WriteLine();
            repositoriDetailBarang.CetakTabel(Entitas2, db, bs => bs.StokBarang > 0);

            var listDetailBarang = repositoriDetailBarang.GetList(db).Where(bs => bs.IdEntitas1 == Entitas2.Id && bs.StokBarang > 0).ToList();

            var index = Utilitas.InputInt("Nomor di atas",
                i => i > 0 && i <= listDetailBarang.Count,
                () => throw new Exception("Proses Berhenti"), "Satuan dengan nomor {0} tidak ada");

            var DetailBarang = listDetailBarang[index - 1];

            if (Entitas1.DaftarDetailTransaksi.Any(dt => dt.IdEntitas2 == DetailBarang.IdEntitas1 
                && dt.IdSatuan == DetailBarang.IdEntitas2))
            {
                Console.WriteLine($"Detail dengan barang '{DetailBarang.NamaBarang}' dan Satuan '{DetailBarang.NamaSatuan}' sudah ada");
                throw new Exception("Proses berhenti");
            }

            int jumlah = Utilitas.InputInt("Jumlah Barang",
                i => i > 0 && i <= DetailBarang.StokBarang, null, "Stok Barang tidak cukup");

            DetailBarang.StokBarang -= jumlah;

            var detailTransaksi = new DetailTransaksi()
            {
                IdEntitas1 = Entitas1.Id,
                IdEntitas2 = Entitas2.Id,
                NamaBarang = Entitas2.NamaBarang,
                HargaBarang = DetailBarang.HargaBarang,
                Jumlah = jumlah,
                Satuan = DetailBarang.NamaSatuan,
                IdSatuan = DetailBarang.IdEntitas2,
                EntitasSatuan = DetailBarang.Entitas2,
                Entitas1 = Entitas1,
                Entitas2 = Entitas2,
            };

            return detailTransaksi;
        }

        public DetailTransaksi InputUpdate(DetailTransaksi entitas, AppDbContext db)
        {
            var repositoriDetailBarang = new RepositoriDetailBarang();

            var stokBarang = repositoriDetailBarang.Get(db, entitas.IdEntitas2, entitas.IdSatuan).StokBarang;
            int jumlah = Utilitas.InputInt("Jumlah Barang", (i) => i < stokBarang + entitas.Jumlah, null, "Stok barang tidak cukup");

            var jumlahLama = entitas.Jumlah;
            var jumlahBaru = jumlah;
            var DetailBarang = repositoriDetailBarang.Get(db, entitas.IdEntitas2, entitas.IdSatuan);
            DetailBarang.StokBarang = DetailBarang.StokBarang - (jumlahBaru - jumlahLama);

            entitas.Jumlah = jumlah;

            return entitas;
        }

        public DetailTransaksi InputDelete(Transaksi Entitas1,  AppDbContext db)
        {
            CetakTabel(Entitas1, db);
            var index = Utilitas.InputInt("Nomor di atas",
                (i) => i > 0 && i <= JumlahEntitas(db, dt => dt.IdEntitas1 == Entitas1.Id),
                () => throw new Exception("Proses Berhenti"),
                "Detail Transaksi dengan nomor {0} tidak ada");

            var detailTransaksi = GetList(db).Where(dt => dt.IdEntitas1 == Entitas1.Id).ToList()[index - 1];

            return detailTransaksi;
        }

        public DetailTransaksi InputDelete(Barang Entitas2, AppDbContext db)
        {
            CetakTabel(Entitas2, db);
            var index = Utilitas.InputInt("Nomor di atas",
                (i) => i > 0 && i <= JumlahEntitas(db, dt => dt.IdEntitas2 == Entitas2.Id),
                () => throw new Exception("Proses Berhenti"),
                "Detail Transaksi dengan nomor {0} tidak ada");

            var detailTransaksi = GetList(db).Where(dt => dt.IdEntitas2 == Entitas2.Id).ToList()[index - 1];

            return detailTransaksi;
        }

        public void Add(DetailTransaksi Entitas, AppDbContext db)
        {
            try
            {
                db.TblDetailTransaksi.Add(Entitas);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Update(DetailTransaksi Entitas, AppDbContext db)
        {
            try
            {
                var repositoriDetailBarang = new RepositoriDetailBarang();

                var detailTransaksi = db.TblDetailTransaksi.Find(Entitas.IdEntitas1, Entitas.IdEntitas2, Entitas.IdSatuan);

                detailTransaksi.Jumlah = Entitas.Jumlah;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(DetailTransaksi entitas, AppDbContext db)
        {
            try
            {
                db.TblDetailTransaksi.Remove(entitas);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CetakTabel(Transaksi Entitas1, AppDbContext db, Func<DetailTransaksi, bool> kondisi = null)
        {
            try
            {
                var listDetailTransaksi = db.TblDetailTransaksi.Where(dt => dt.IdEntitas1 == Entitas1.Id).ToList();

                if(kondisi != null)
                    listDetailTransaksi = listDetailTransaksi.Where(dt => kondisi(dt)).ToList();

                Console.WriteLine(Utilitas.BuatTabel(listDetailTransaksi, new DetailTransaksi().ListKolom, true));
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public void CetakTabel(Barang Entitas2, AppDbContext db, Func<DetailTransaksi, bool> kondisi = null)
        {
            try
            {
                var listDetailTransaksi = db.TblDetailTransaksi.Where(dt => dt.IdEntitas2 == Entitas2.Id).ToList();

                if (kondisi != null)
                    listDetailTransaksi = listDetailTransaksi.Where(dt => kondisi(dt)).ToList();

                Console.WriteLine(Utilitas.BuatTabel(listDetailTransaksi, new DetailTransaksi().ListKolom, true));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetailTransaksi> GetList(AppDbContext db)
        {
            return db.TblDetailTransaksi.ToList();
        }

        public DetailTransaksi Get(AppDbContext db, params string[] ids)
        {
            if (ids.Length < 3)
                throw new Exception("Jumlah id kurang dari 3");
            return db.TblDetailTransaksi.Find(ids as object[]);
        }

        public bool IsExist(AppDbContext db, params string[] ids)
        {
            if (ids.Length < 3)
                throw new Exception("Jumlah id kurang dari 3");

            return db.TblDetailTransaksi.Find(ids as object[]) != null;
        }

        public int JumlahEntitas(AppDbContext db, Func<DetailTransaksi, bool> kondisi = null)
        {
            try
            {
                var list = GetList(db);

                if(kondisi != null)
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
