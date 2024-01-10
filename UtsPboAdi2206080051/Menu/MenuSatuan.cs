using UtsPboAdi2206080051.Entitas.EntitasSatuan;
using UtsPboAdi2206080051.EntitasSatuan;
using UtsPboAdi2206080051.Menu.Commons;

namespace UtsPboAdi2206080051.Menu
{
    public class MenuSatuan : BaseMenu<Satuan>
    {
        public MenuSatuan() : base(new RepositoriSatuan(), nameof(Satuan), new AppDbContext())
        {

        }
    }
}
