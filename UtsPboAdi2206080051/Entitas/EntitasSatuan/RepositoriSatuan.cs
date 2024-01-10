using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtsPboAdi2206080051.Entitas.Commons;
using UtsPboAdi2206080051.Entitas.EntitasBarang;
using UtsPboAdi2206080051.EntitasSatuan;

namespace UtsPboAdi2206080051.Entitas.EntitasSatuan
{
    public class RepositoriSatuan : IRepositori<Satuan>
    {
        public Satuan InputCreate(AppDbContext db)
        {
            string id, nama;

            id = Utilitas.InputString("ID", (s) => !IsExist(s, db), null, "Satuan sudah ada");
            nama = Utilitas.InputString("Nama Satuan", (s) => s.Length <= 40, null, "Panjang Nama lebih dari 40");

            var satuan = new Satuan()
            {
                Id = id,
                NamaSatuan = nama,
            };

            return satuan;
        }

        public Satuan InputUpdate(AppDbContext db)
        {
            string id, nama;

            id = Utilitas.InputString("ID", (s) => IsExist(s, db),
                () => throw new Exception("Proses Berhenti"), 
                "Satuan tidak ada");

            Console.WriteLine("Masukan Data Baru");
            nama = Utilitas.InputString("Nama Satuan", (s) => s.Length <= 40, null, "Panjang Nama lebih dari 40");

            var satuan = new Satuan()
            {
                Id = id,
                NamaSatuan = nama,
            };

            return satuan;
        }

        public Satuan InputDelete(AppDbContext db)
        {
            string id;

            id = Utilitas.InputString("ID", s => IsExist(s, db),
                () => throw new Exception("Proses Berhenti"),
                "Satuan tidak ada");

            return Get(id, db);
        }

        public void Add(Satuan Entitas, AppDbContext db)
        {
            try
            {
                db.TblSatuan.Add(Entitas);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Satuan Entitas, AppDbContext db)
        {
            try
            {
                var satuan = Get(Entitas.Id, db);
                satuan.NamaSatuan = Entitas.NamaSatuan;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(Satuan entitas, AppDbContext db)
        {
            try
            {
                if(entitas.DaftarDetailBarang != null && entitas.DaftarDetailBarang.Count > 0)
                    Console.WriteLine($"{entitas.DaftarDetailBarang.Count} detail barang ikut terhapus");
                db.TblSatuan.Remove(entitas);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CetakTabel(AppDbContext db, Func<Satuan, bool> kondisi = null)
        {
            try
            {
                var listSatuan = GetList(db);
                var listKolom = new Satuan().ListKolom;
                if (kondisi != null)
                    listSatuan = listSatuan.Where(b => kondisi(b)).ToList();
                Console.Write(Utilitas.BuatTabel(listSatuan, listKolom));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Satuan> GetList(AppDbContext db)
        {
            try
            {
                var listSatuan = db.TblSatuan.ToList();
                return listSatuan;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Satuan Get(string id, AppDbContext db)
        {
            try
            {
                return db.TblSatuan.Find(id);
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

        public int JumlahEntitas(AppDbContext db, Func<Satuan, bool> kondisi = null)
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
