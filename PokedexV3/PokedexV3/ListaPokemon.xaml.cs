using Newtonsoft.Json;
using PokedexV3.Models;
using PokedexV3.Models.Info;
using PokedexV3.Models.Lista;
using PokedexV3.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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


            this.IsVisible = false;

            LoadComponents().ContinueWith(task =>
            {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Device.BeginInvokeOnMainThread(() => { this.IsVisible = true; });
                }
            });
        }



        private async Task LoadComponents()
        {
            await GetPokemon(URL);
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

            var tappedItem = e.Item as PokeImgModel;
            var x = tappedItem.Url.ToString();
            var y = tappedItem.Name.ToString();
            if (x == "https://pokeapi.co/api/v2/pokemon-species/133/")
            {
                await CheckConnAsync(null, new VistaEevee(y), false);
            }
            else
            {
                await CheckConnAsync(new VistaPokemon(y, x), null, true);
            }
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string res = await DisplayActionSheet("Filtrar por Generacion", "Cerrar", null,
                "Todos - Nacional",
                "Generacion 1 - Región Kanto",
                "Generacion 2 - Región Jhoto",
                "Generacion 3 - Región Hoenn",
                "Generacion 4 - Región Sinnoh",
                "Generacion 5 - Región Unova",
                "Generacion 6 - Región Kalos",
                "Generacion 7 - Región Alola",
                "Generacion 8 - Región Galar");
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
            Dictionary<string, string> generaciones = new Dictionary<string, string>()
            {
                { "Todos - Nacional", URL },
                { "Generacion 1 - Región Kanto", "https://pokeapi.co/api/v2/generation/1" },
                { "Generacion 2 - Región Jhoto", "https://pokeapi.co/api/v2/generation/2" },
                { "Generacion 3 - Región Hoenn", "https://pokeapi.co/api/v2/generation/3" },
                { "Generacion 4 - Región Sinnoh", "https://pokeapi.co/api/v2/generation/4" },
                { "Generacion 5 - Región Unova", "https://pokeapi.co/api/v2/generation/5" },
                { "Generacion 6 - Región Kalos", "https://pokeapi.co/api/v2/generation/6" },
                { "Generacion 7 - Región Alola", "https://pokeapi.co/api/v2/generation/7" },
                { "Generacion 8 - Región Galar", "https://pokeapi.co/api/v2/generation/8" }
            };

            if (generaciones.TryGetValue(res, out string url))
            {
                if (res == "Todos - Nacional")
                {
                    await GetPokemon(url);
                }
                else
                {
                    await GetGenPokemon(url);
                }

                this.Title = res;
            }
        }


        private async Task CheckConnAsync(VistaPokemon vista, VistaEevee vistaEevee, bool vistaNormal)
        {
            // Intenta establecer la conexión de nuevo aquí
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                // Hay conexión a Internet, cambia a la vista normal

                if (vistaNormal) await Navigation.PushAsync(vista);
                else await Navigation.PushAsync(vistaEevee);

            }
            else
            {
                await Navigation.PushAsync(new NoConexion());
            }
        }
    }
}