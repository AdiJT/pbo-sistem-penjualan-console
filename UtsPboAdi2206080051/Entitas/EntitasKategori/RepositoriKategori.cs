using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtsPboAdi2206080051.Entitas.Commons;
using UtsPboAdi2206080051.Entitas.EntitasBarang;

namespace UtsPboAdi2206080051.Entitas.EntitasKategori
{
    public class RepositoriKategori : IRepositori<Kategori>
    {
        public Kategori InputCreate(AppDbContext db)
        {
            string id, nama;

            id = Utilitas.InputString("ID", s => !IsExist(s, db), null, "Kategori dengan ID {0} sudah ada");
            nama = Utilitas.InputString("Nama Kategori", (s) => s.Length <= 40, null, "Panjang Nama lebih dari 40");

            var kategori = new Kategori()
            {
                Id = id,
                NamaKategori = nama,
            };

            return kategori;
        }

        public Kategori InputUpdate(AppDbContext db)
        {
            string id, nama;

            id = Utilitas.InputString("ID", s => IsExist(s, db), () => throw new Exception("Proses Berhenti"), "Kategori tidak ada");
            Console.WriteLine("Masukan Data Baru");
            nama = Utilitas.InputString("Nama Kategori", (s) => s.Length <= 40, null, "Panjang Nama lebih dari 40");

            var kategori = new Kategori()
            {
                Id = id,
                NamaKategori = nama,
            };

            return kategori;
        }

        public Kategori InputDelete(AppDbContext db)
        {
            string id;

            id = Utilitas.InputString("ID", s => IsExist(s, db), () => throw new Exception("Proses Berhenti"), "Kategori tidak ada");

            return Get(id, db);
        }

        public void Add(Kategori Entitas, AppDbContext db)
        {
            try
            {
                db.TblKategori.Add(Entitas);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Kategori Entitas, AppDbContext db)
        {
            try
            {
                var kategori = Get(Entitas.Id, db);
                kategori.NamaKategori = Entitas.NamaKategori;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(Kategori entitas, AppDbContext db)
        {
            try
            {
                var repositoriBarang = new RepositoriBarang();

                if (entitas.DaftarBarang != null && entitas.DaftarBarang.Count > 0)
                {
                    Console.WriteLine($"{entitas.DaftarBarang.Count} barang ikut tehapus");
                    var listBarang = entitas.DaftarBarang.ToList();
                    foreach (var item in listBarang)
                        repositoriBarang.Delete(item, db);
                    Update(entitas, db);
                }

                db.TblKategori.Remove(entitas);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CetakTabel(AppDbContext db, Func<Kategori, bool> kondisi = null)
        {
            try
            {
                var listKategori = GetList(db);
                var listKolom = new Kategori().ListKolom;
                if (kondisi != null)
                    listKategori = listKategori.Where(b => kondisi(b)).ToList();
                Console.Write(Utilitas.BuatTabel(listKategori, listKolom));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Kategori> GetList(AppDbContext db)
        {
            try
            {
                var listKategori = db.TblKategori.ToList();
                return listKategori;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Kategori Get(string id, AppDbContext db)
        {
            try
            {
                return db.TblKategori.Find(id);
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

        public int JumlahEntitas(AppDbContext db, Func<Kategori, bool> kondisi = null)
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
