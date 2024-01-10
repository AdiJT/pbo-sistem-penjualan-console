using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtsPboAdi2206080051.Entitas.Commons
{
    //Model entitas pada database
    public abstract class BaseEntitas
    {
        public BaseEntitas()
        {
            ListKolom.Add(new Kolom
            {
                NamaEntitas = nameof(BaseEntitas),
                NamaProperti = nameof(Id),
                NamaKolom = nameof(Id),
                PanjangKolom = 10,
                FormatString = ""
            });
        }

        [Key]
        public string Id { get; set; }

        //Kolom - Kolom yang ingin ditampilkan dalam bentuk kolom 
        [NotMapped]
        public List<Kolom> ListKolom { get; } = new List<Kolom>();
    }
}
