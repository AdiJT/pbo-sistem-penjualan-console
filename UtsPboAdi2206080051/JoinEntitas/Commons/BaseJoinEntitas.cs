using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtsPboAdi2206080051.Entitas.Commons;

namespace UtsPboAdi2206080051.JoinEntitas.Commons
{
    //Model database bagi entitas yang merupakan tabel hasil relasi many to many
    public abstract class BaseJoinEntitas<T1, T2> where T1 : class where T2 : class
    {
        public BaseJoinEntitas()
        {
        }

        [ForeignKey(nameof(Entitas1))]
        [Key, Column(Order = 0)]
        public string IdEntitas1 { get; set; }

        [ForeignKey(nameof(Entitas2))]
        [Key, Column(Order = 1)]
        public string IdEntitas2 { get; set; }

        public virtual T1 Entitas1 { get; set; }
        public virtual T2 Entitas2 { get; set; }

        [NotMapped]
        public List<Kolom> ListKolom { get; } = new List<Kolom>();
    }
}
