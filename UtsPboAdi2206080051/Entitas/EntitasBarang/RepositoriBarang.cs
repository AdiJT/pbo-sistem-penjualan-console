using System;
using System.Collections.Generic;
using System.Linq;
using UtsPboAdi2206080051.Entitas.Commons;
using UtsPboAdi2206080051.Entitas.EntitasKategori;
using UtsPboAdi2206080051.Entitas.EntitasSatuan;
using UtsPboAdi2206080051.JoinEntitas.EntitasDetailBarang;

namespace UtsPboAdi2206080051.Entitas.EntitasBarang
{
    public class RepositoriBarang : IRepositori<Barang>
    {
        public Barang InputCreate(AppDbContext db)
        {
            var repositoriKategori = new RepositoriKategori();
            var repositoriSatuan = new RepositoriSatuan();

            string id, nama, idKategori;
            Kategori kategori = new Kategori();

            if (repositoriKategori.GetList(db).Count() == 0)
                throw new Exception("Tabel Kategori masih kosong");

            if (repositoriSatuan.GetList(db).Count() == 0)
                throw new Exception("Tabel Satuan masih kosong");

            id = Utilitas.InputString("ID", s => !IsExist(s, db), null, "Barang sudah ada");
            nama = Utilitas.InputString("Nama Barang", (s) => s.Length <= 40, null, "Panjang Nama lebih dari 40");

            Console.WriteLine();
            repositoriKategori.CetakTabel(db);
            idKategori = Utilitas.InputString("ID Kategori", s => repositoriKategori.IsExist(s, db), null, "Kategori tidak ada");

            kategori = repositoriKategori.Get(idKategori, db);

            var barang = new Barang()
            {
                Id = id,
                NamaBarang = nama,
                IdKategori = idKategori,
                Kategori = kategori,
                DaftarDetailBarang = new List<DetailBarang>()
            };

            return barang;
        }

        public Barang InputUpdate(AppDbContext db)
        {
            var repositoriKategori = new RepositoriKategori();
            string id, nama, idKategori;
            Kategori kategori = new Kategori();

            id = Utilitas.InputString("ID", s => IsExist(s, db), () => throw new Exception("Proses Dihentikan"), "Barang tidak ada");
            var barang = Get(id, db);

            Console.WriteLine("Masukan data baru");

            nama = Utilitas.InputString("Nama Barang", (s) => s.Length <= 40, null, "Panjang Nama lebih dari 50");

            Console.WriteLine();
            repositoriKategori.CetakTabel(db);
            idKategori = Utilitas.InputString("ID Kategori", s => repositoriKategori.IsExist(s, db), () => throw new Exception("Proses Berhenti"), "Kategori tidak ada");

            kategori = repositoriKategori.Get(id, db);

            barang.NamaBarang = nama;
            barang.IdKategori = idKategori;

            return barang;
        }

        public Barang InputDelete(AppDbContext db)
        {
            string id;

            id = Utilitas.InputString("ID", s => IsExist(s, db), () => throw new Exception("Proses Berhenti"), "Barang tidak ada");

            return Get(id, db);
        }

        public void Add(Barang entitas, AppDbContext db)
        {
            try
            {
                db.TblBarang.Add(entitas);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Barang entitas, AppDbContext db)
        {
            try
            {
                var barang = Get(entitas.Id, db);
                barang.NamaBarang = entitas.NamaBarang;
                barang.IdKategori = entitas.IdKategori;
                barang.DaftarDetailBarang = entitas.DaftarDetailBarang;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(Barang entitas, AppDbContext db)
        {
            try
            {
                if (entitas.DaftarDetailBarang != null && entitas.DaftarDetailBarang.Count > 0)
                    Console.WriteLine($"{entitas.DaftarDetailBarang.Count} detail barang ikut terhapus");

                db.TblBarang.Remove(entitas);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CetakTabel(AppDbContext db, Func<Barang, bool> kondisi = null)
        {
            try
            {
                var listBarang = GetList(db);
                var listKolom = new Barang().ListKolom;

                if (kondisi != null)
                    listBarang = listBarang.Where(b => kondisi(b)).ToList();

                Console.Write(Utilitas.BuatTabel(listBarang, listKolom));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Barang> GetList(AppDbContext db)
        {
            try
            {
                var listBarang = db.TblBarang.ToList();
                return listBarang;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Barang TambahSatuan(Barang barang, AppDbContext db)
        {
            try
            {
                var repositoriSatuan = new RepositoriSatuan();
                var repositoriDetailBarang = new RepositoriDetailBarang();
                repositoriSatuan.CetakTabel(db, s => repositoriDetailBarang.IsExist(db, barang.Id, s.Id) == false);
                while(barang.DaftarDetailBarang.Count < repositoriSatuan.GetList(db).Count)
                {
                    var idSatuan = Utilitas.InputString("ID Satuan",
                        s => repositoriSatuan.IsExist(s, db), null,
                        "Tidak ada satuan dengan ID '{0}'"
                        );

                    var satuan = repositoriSatuan.Get(idSatuan, db);
                    var DetailBarang = repositoriDetailBarang.InputCreate(barang, satuan, db);
                    barang.DaftarDetailBarang.Add(DetailBarang);
                    
                    Console.Write("Tambah Satuan[y/n] : ");
                    var pilih = Console.ReadLine().Trim().ToLower();

                    if (pilih != "y")
                        break;
                }

                return barang;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Barang Get(string id, AppDbContext db)
        {
            try
            {
                return db.TblBarang.Find(id);
            }
            catch(Exception ex)
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

        public int JumlahEntitas(AppDbContext db, Func<Barang, bool> kondisi = null)
        {
            try
            {
                var list = GetList(db);
                if (kondisi != null)
                    list = list.Where(e => kondisi(e)).ToList();
                return list.Count();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
