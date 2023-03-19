
using PokedexV3.Vistas;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokedexV3
{
    public partial class App : Application
    {
        public App()
        {   
            InitializeComponent();

            CheckConn();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        private void CheckConn()
        {
            // Intenta establecer la conexión de nuevo aquí
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                // Hay conexión a Internet, cambia a la vista normal
                MainPage = new NavigationPage(new ListaPokemon());
                var navigationPage = new NavigationPage();
            }
            else
            {
                MainPage = new NavigationPage(new NoConexion());
                var navigationPage = new NavigationPage();
            }
        }
    }
}
