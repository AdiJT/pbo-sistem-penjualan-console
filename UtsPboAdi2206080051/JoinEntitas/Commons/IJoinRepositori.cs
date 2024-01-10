using System;
using System.Collections.Generic;
using UtsPboAdi2206080051.Entitas.Commons;

namespace UtsPboAdi2206080051.JoinEntitas.Commons
{
    //iRepositori bagi join entity
    public interface IJoinRepositori<T1, T2, TJoin> where T1 : class
                                                    where T2 : class
                                                    where TJoin : BaseJoinEntitas<T1, T2>
    {
        TJoin InputCreate(T1 entitas1, T2 entitas2, AppDbContext db);
        TJoin InputUpdate(TJoin entitas, AppDbContext db);
        TJoin InputDelete(T1 entitas1, AppDbContext db);
        TJoin InputDelete(T2 entitas2, AppDbContext db);

        void Add(TJoin entitas, AppDbContext db);
        void Update(TJoin entitas, AppDbContext db);
        void Delete(TJoin entitas, AppDbContext db);

        void CetakTabel(T1 entitas1, AppDbContext db, Func<TJoin, bool> kondisi = null);
        void CetakTabel(T2 entitas2, AppDbContext db, Func<TJoin, bool> kondisi = null);

        List<TJoin> GetList(AppDbContext db);
        TJoin Get(AppDbContext db, params string[] ids);

        bool IsExist(AppDbContext db, params string[] ids);
        int JumlahEntitas(AppDbContext db, Func<TJoin, bool> kondisi = null);
    }
}
