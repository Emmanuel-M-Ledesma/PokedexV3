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
        public string Generacion;
        List<PokeImgModel> listaPokemon = new List<PokeImgModel>();
        public ListaPokemon()
        {
            InitializeComponent();
            ToolbarItem item = new ToolbarItem
            {
                Text = "Buscar",
                Priority = 5,
                Order = ToolbarItemOrder.Secondary
            };
            URL = "https://pokeapi.co/api/v2/pokedex/1";
            UrlDesc = "https://pokeapi.co/api/v2/pokemon-species/";  
            _ = GetPokemon(URL);
            BackgroundColor = Color.Black;
        }
        public async Task<List<PokeImgModel>> GetPokemon(string url)
        {
            listaPokemon.Clear();
            HttpClient http = new HttpClient();
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
                    listaPokemon.Add(listModel);                   
                    
                }
                ListPoke.ItemsSource = null;
                ListPoke.ItemsSource = listaPokemon;
                ListPoke.BackgroundColor = Color.Black;
                ListPoke.SeparatorColor = Color.Wheat;
            }
            return listaPokemon;
        }
        public async Task<List<PokeImgModel>> GetGenPokemon(string Url)
        {
            listaPokemon.Clear();
            HttpClient http = new HttpClient();
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

                    listaPokemon.Add(generationModel);
                }
                ListPoke.ItemsSource = null;
                ListPoke.ItemsSource = listaPokemon;
                ListPoke.BackgroundColor = Color.Black;
                ListPoke.SeparatorColor = Color.Wheat;
            }

            return listaPokemon;
        }

        private void Buscar_TextChanged(object sender, TextChangedEventArgs e)
        {

            var palabra = Buscar.Text;
            var item = listaPokemon;
            if (!string.IsNullOrEmpty(palabra))
            {
                ListPoke.ItemsSource = item.Where(x => x.Name.ToLower().Contains(palabra.ToLower()));
            }
            else
            {
                ListPoke.ItemsSource = listaPokemon;
            }
            

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
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string res = await DisplayActionSheet("Filtrar por Generacion", "Cerrar", null, "Todos", "Generacion 1", "Generacion 2", "Generacion 3", "Generacion 4", "Generacion 5", "Generacion 6", "Generacion 7", "Generacion 8");
            TraerGeneracion(res);
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
        private void BuscarClicked(object sender, EventArgs e)
        {
            Mostrarbarra();
        }

        private async void TraerGeneracion(string res)
        {
            switch (res)
            {
                case "Todos":
                    await GetPokemon(URL);
                    this.Title = "Lista Pokémon - Nacional";
                    break;
                case "Generacion 1":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/1");
                    this.Title = "Generacion 1 - Región Kanto";
                    break;
                case "Generacion 2":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/2");
                    this.Title = "Generacion 2 - Región Jhoto";
                    break;
                case "Generacion 3":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/3");
                    this.Title = "Generacion 3 - Región Hoenn";
                    break;
                case "Generacion 4":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/4");
                    this.Title = "Generacion 4 - Región Sinnoh";
                    break;
                case "Generacion 5":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/5");
                    this.Title = "Generacion 5 - Región Unova";
                    break;
                case "Generacion 6":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/6");
                    this.Title = "Generacion 6 - Región Kalos";
                    break;
                case "Generacion 7":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/7");
                    this.Title = "Generacion 7 - Región Alola";
                    break;
                case "Generacion 8":
                    await GetGenPokemon("https://pokeapi.co/api/v2/generation/8");
                    this.Title = "Generacion 8 - Región Galar";
                    break;

            }
        }
    }
}