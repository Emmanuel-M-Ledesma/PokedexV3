using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokedexV3.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoConexion : ContentPage
    {
        public NoConexion()
        {
            InitializeComponent();            
        }

        

        private void Button_Clicked(object sender, EventArgs e)
        {
            CheckConn();
        }

        private void CheckConn()
        {
            // Intenta establecer la conexión de nuevo aquí
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                // Hay conexión a Internet, cambia a la vista normal
                App.Current.MainPage = new NavigationPage(new ListaPokemon());
                
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new NoConexion());
                
            }
        }
    }
}