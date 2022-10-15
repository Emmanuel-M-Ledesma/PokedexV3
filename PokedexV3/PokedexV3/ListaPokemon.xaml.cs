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
            if (Buscar.IsVisible)
            {
                Buscar.IsVisible = false;
                ToolbarItems[0].IconImageSource = "SearchIcon.png";
            }
            var tappedItem = e.Item as PokeImgModel;
            string x = tappedItem.Name;            
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

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string res = await DisplayActionSheet("Filtrar por Generacion", "Cerrar", null,"Todos", "Generacion 1", "Generacion 2", "Generacion 3", "Generacion 4", "Generacion 5", "Generacion 6", "Generacion 7", "Generacion 8");
            switch (res)
            {
                case "Todos":
                    await GetPokemon(URL);
                    break;
                case "Generacion 1":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/1");
                    break;
                case "Generacion 2":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/2");
                    break;
                case "Generacion 3":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/3");
                    break;
                case "Generacion 4":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/4");
                    break;
                case "Generacion 5":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/5");
                    break;
                case "Generacion 6":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/6");
                    break;
                case "Generacion 7":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/7");
                    break;
                case "Generacion 8":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/8");
                    break;

            }
        }
        public async Task<bool> GetGenPokemon(string Url)
        {
            HttpClient http = new HttpClient();
            List<PokeImgModel> genList = new List<PokeImgModel>();
            var respuesta = await http.GetAsync(Url);
            if (respuesta.IsSuccessStatusCode)
            {
                var respString = await respuesta.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<GenerationModel>(respString);
                foreach (var genPoke in json.PokemonSpecies)
                {
                    PokeImgModel generationModel = new PokeImgModel()
                    {
                        Name = genPoke.Name,
                        Url = genPoke.Url,
                        UrlImg = "https://img.pokemondb.net/sprites/home/normal/" + genPoke.Name + ".png"
                    };

                    genList.Add(generationModel);
                }
                
                ListPoke.ItemsSource = genList;
                ListPoke.BackgroundColor = Color.Black;
                ListPoke.SeparatorColor = Color.Wheat;
            }
            return true;
        }
        private void Buscar_SearchButtonPressed(object sender, EventArgs e)
        {

        }
    }
}