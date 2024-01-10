using UtsPboAdi2206080051.Entitas.EntitasKategori;
using UtsPboAdi2206080051.Menu.Commons;

namespace UtsPboAdi2206080051.Menu
{
    public class MenuKategori : BaseMenu<Kategori>
    {
        public MenuKategori() : base(new RepositoriKategori(), nameof(Kategori), new AppDbContext())
        {

        }
    }
}
