using Newtonsoft.Json;
using PokedexV3.Models;
using PokedexV3.Models.Info;
using PokedexV3.Models.Lista;
using PokedexV3.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PokedexV3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListaPokemon : ContentPage
    {
        public string URL;
        public string Siguiente;
        public string Previo;
        public string UrlDesc;
        public ListaPokemon()
        {
            InitializeComponent();
            ToolbarItem item = new ToolbarItem
            {
                Text = "Buscar",
                Priority = 5,
                Order = ToolbarItemOrder.Secondary
            };
            //item.Clicked += BuscarClicked;
            URL = "https://pokeapi.co/api/v2/pokedex/1";
            UrlDesc = "https://pokeapi.co/api/v2/pokemon-species/";
            _ = GetPokemon(URL);
            BackgroundColor = Color.Black;
            

        }

        private void BuscarClicked(object sender, EventArgs e)
        {
            Mostrarbarra();
            
        }
        private void FiltrarClicked(Object sender, EventArgs e)
        {

        }

        public async Task<bool> GetPokemon(string url)
        {

            HttpClient http = new HttpClient();
            List<PokeImgModel> pokelist = new List<PokeImgModel>();
            var respuesta = await http.GetAsync(url);
            if (respuesta.IsSuccessStatusCode)
            {

                var respString = await respuesta.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<PokedexModel>(respString);                

                foreach (var poke in json.PokemonEntries)
                {

                    PokeImgModel listModel = new PokeImgModel
                    {
                        Name = poke.PokemonSpecies.Name.ToUpper(),
                        Url = poke.PokemonSpecies.Url,
                        UrlImg = "https://img.pokemondb.net/sprites/home/normal/" + poke.PokemonSpecies.Name + ".png"
                    };
                    

                    pokelist.Add(listModel);
                }   

                ListPoke.ItemsSource = pokelist;
                ListPoke.BackgroundColor = Color.Black;
                ListPoke.SeparatorColor = Color.Wheat;
            }
            return true;
        }
        
        public async Task<string> getColor(long orden)
        {
            string color = "";
            var Detalle = UrlDesc + orden;
            HttpClient http = new HttpClient();
            DetalleModel PokeDetail = new DetalleModel();
            var resp = await http.GetAsync(Detalle);
            if (resp.IsSuccessStatusCode)
            {
                var respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<DetalleModel>(respString);

                PokeDetail.Color = json.Color;
                color = PokeDetail.Color.Name;
            }
            return color;
        }

        private async void ListPoke_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedItem = e.Item as PokeImgModel;
            string x = tappedItem.Name;
            Mostrarbarra();
            await Navigation.PushAsync(new VistaPokemon(x));
        }
        private void Mostrarbarra()
        {
            if (Buscar.IsVisible)
            {
                Buscar.IsVisible = false;
                ToolbarItems[0].IconImageSource = "SearchIcon.png";
            }
            else
            {
                Buscar.IsVisible = true;
                ToolbarItems[0].IconImageSource = "CloseIcon.png";
            }

        }

    }
}