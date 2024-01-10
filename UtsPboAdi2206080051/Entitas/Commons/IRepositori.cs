using System;
using System.Collections.Generic;

namespace UtsPboAdi2206080051.Entitas.Commons
{
    //Digunakan untuk mengakses data pada database melalui DbContext dan DbSet
    public interface IRepositori<T> where T : BaseEntitas
    {
        //Method inputan data objek
        T InputCreate(AppDbContext db);
        T InputUpdate(AppDbContext db);
        T InputDelete(AppDbContext db);

        //Method yang memanipulasi data berisi db.SaveChanges
        void Add(T Entitas, AppDbContext db);
        void Update(T Entitas, AppDbContext db);
        void Delete(T entitas, AppDbContext db);

        //Mengouputkan data
        void CetakTabel(AppDbContext db, Func<T, bool> kondisi = null);

        //Method Mengambil data
        List<T> GetList(AppDbContext db); // list kosong jika tidak ada
        T Get(string id, AppDbContext db); //null jika tidak ada

        //Method tambahan
        bool IsExist(string id, AppDbContext db); //true jika ada, false jika tidak ada
        int JumlahEntitas(AppDbContext db, Func<T, bool> kondisi = null);

    }
}
