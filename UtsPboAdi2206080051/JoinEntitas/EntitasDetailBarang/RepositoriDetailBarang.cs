using System;
using System.Collections.Generic;
using System.Linq;
using UtsPboAdi2206080051.Entitas.EntitasBarang;
using UtsPboAdi2206080051.EntitasSatuan;
using UtsPboAdi2206080051.JoinEntitas.Commons;

namespace UtsPboAdi2206080051.JoinEntitas.EntitasDetailBarang
{
    public class RepositoriDetailBarang : IJoinRepositori<Barang, Satuan, DetailBarang>
    {
        public DetailBarang InputCreate(Barang entitas1, Satuan entitas2, AppDbContext db)
        {
            if (IsExist(db, entitas1.Id, entitas2.Id))
                throw new Exception($"Barang '{entitas1.NamaBarang}' dengan satuan '{entitas2.NamaSatuan}' sudah ada");

            var hargaBarang = Utilitas.InputDecimal("Harga Barang", d => d > 0, "Harga barang kurang dari 0");
            var stok = Utilitas.InputInt("Stok Barang", i => i > 0, null, "Stok Kurang dari 0");

            var detailBarang = new DetailBarang()
            {
                IdEntitas1 = entitas1.Id,
                IdEntitas2 = entitas2.Id,
                HargaBarang = hargaBarang,
                StokBarang = stok,
                Entitas1 = entitas1,
                Entitas2 = entitas2,
            };

            return detailBarang;
        }

        public DetailBarang InputUpdate(DetailBarang entitas, AppDbContext db)
        {
            if (!IsExist(db, entitas.IdEntitas1, entitas.IdEntitas2))
                throw new Exception($"Barang '{entitas.NamaBarang}' dengan satuan '{entitas.NamaSatuan}' tidak ada");

            var hargaBarang = Utilitas.InputDecimal("Harga Barang", d => d > 0, "Harga barang kurang dari 0");
            var stok = Utilitas.InputInt("Stok Barang", i => i > 0, null, "Stok Kurang dari 0");

            var detailBarang = new DetailBarang()
            {
                IdEntitas1 = entitas.IdEntitas1,
                IdEntitas2 = entitas.IdEntitas2,
                HargaBarang = hargaBarang,
                StokBarang = stok,
                Entitas1 = entitas.Entitas1,
                Entitas2 = entitas.Entitas2,
            };

            return detailBarang;
        }

        public DetailBarang InputDelete(Barang Entitas1, AppDbContext db)
        {
            CetakTabel(Entitas1, db);
            var listDetailBarang = GetList(db).Where(bs => bs.IdEntitas1 == Entitas1.Id).ToList();
            var index = Utilitas.InputInt("Nomor di atas", 
                (i) => i > 0 && i <= listDetailBarang.Count, 
                () => throw new Exception("Proses Berhenti"), 
                "Barang dan Satuan dengan nomor {0} tidak ada");

            var detailBarang = listDetailBarang[index - 1];

            return detailBarang;
        }

        public DetailBarang InputDelete(Satuan Entitas2, AppDbContext db)
        {
            CetakTabel(Entitas2, db);
            var listDetailBarang = GetList(db).Where(bs => bs.IdEntitas2 == Entitas2.Id).ToList();
            var index = Utilitas.InputInt("Nomor di atas",
                (i) => i > 0 && i <= listDetailBarang.Count,
                () => throw new Exception("Proses Berhenti"),
                "Barang dan Satuan dengan nomor {0} tidak ada");

            var detailBarang = listDetailBarang[index - 1];

            return detailBarang;
        }

        public void Add(DetailBarang entitas, AppDbContext db)
        {
            try
            {
                db.TblDetailBarang.Add(entitas);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Update(DetailBarang entitas, AppDbContext db)
        {
            try
            {
                var detailBarang = Get(db, entitas.IdEntitas1, entitas.IdEntitas2);
                detailBarang.HargaBarang = entitas.HargaBarang;
                detailBarang.StokBarang = entitas.StokBarang;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(DetailBarang entitas, AppDbContext db)
        {
            try
            {
                db.TblDetailBarang.Remove(entitas);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CetakTabel(Barang entitas1, AppDbContext db, Func<DetailBarang, bool> kondisi = null)
        {
            try
            {
                var listDetailBarang = GetList(db).Where(bs => bs.IdEntitas1 == entitas1.Id).ToList();
                var listKolom = new DetailBarang().ListKolom;

                if(kondisi != null)
                    listDetailBarang = listDetailBarang.Where(ds => kondisi(ds)).ToList();

                Console.WriteLine(Utilitas.BuatTabel(listDetailBarang, listKolom, true));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CetakTabel(Satuan entitas2, AppDbContext db, Func<DetailBarang, bool> kondisi = null)
        {
            try
            {
                var listDetailBarang = GetList(db).Where(bs => bs.IdEntitas2 == entitas2.Id).ToList();
                var listKolom = new DetailBarang().ListKolom;

                if (kondisi != null)
                    listDetailBarang = listDetailBarang.Where(bs => kondisi(bs)).ToList();

                Console.WriteLine(Utilitas.BuatTabel(listDetailBarang, listKolom, true));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DetailBarang Get(AppDbContext db, params string[] ids)
        {
            if (ids.Length < 2)
                throw new ArgumentException("Jumlah id kurang dari 2");
            return db.TblDetailBarang.Find(ids as object[]);
        }

        public List<DetailBarang> GetList(AppDbContext db)
        {
            return db.TblDetailBarang.ToList();
        }

        public bool IsExist(AppDbContext db, params string[] ids)
        {
            if (ids.Length < 2)
                throw new ArgumentException("Jumlah id kurang dari 2");
            return db.TblDetailBarang.Find(ids as object[]) != null;
        }

        public int JumlahEntitas(AppDbContext db, Func<DetailBarang, bool> kondisi = null)
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
