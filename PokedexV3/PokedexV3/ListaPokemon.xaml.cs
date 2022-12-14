using Newtonsoft.Json;
using PokedexV3.Models;
using PokedexV3.Models.Info;
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
            URL = "https://pokeapi.co/api/v2/pokemon";
            UrlDesc = "https://pokeapi.co/api/v2/pokemon-species/";
            _ = GetPokemon(URL);
            BackgroundColor = Color.Black;

        }
        public async Task<bool> GetPokemon(string url)
        {

            HttpClient http = new HttpClient();
            List<PokeImgModel> pokelist = new List<PokeImgModel>();
            var respuesta = await http.GetAsync(url);
            if (respuesta.IsSuccessStatusCode)
            {

                var respString = await respuesta.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<PokeModel>(respString);
                Siguiente = json.Next;
                Previo = json.Previous;
                if (string.IsNullOrEmpty(Previo))
                {
                    btPrev.IsVisible = false;
                }
                else
                {
                    btPrev.IsVisible = true;
                }
                if (string.IsNullOrEmpty(Siguiente))
                {
                    btNext.IsVisible = false;
                }
                else
                {
                    btNext.IsVisible = true;
                }
                foreach (var poke in json.Results)
                {

                    PokeImgModel listModel = new PokeImgModel();
                    listModel.Name = poke.Name.ToUpper();
                    listModel.Url = poke.Url;
                    listModel.UrlImg = "https://img.pokemondb.net/sprites/home/normal/" + poke.Name + ".png";
                    var  order = await getOrder("https://pokeapi.co/api/v2/pokemon/" + poke.Name, 0);
                    listModel.frColor = await getColor(order);
                    pokelist.Add(listModel);
                }   

                ListPoke.ItemsSource = pokelist;
                ListPoke.BackgroundColor = Color.Black;
                ListPoke.SeparatorColor = Color.Wheat;
            }
            return true;
        }
        public async Task<long> getOrder(string Url, long orden)
        {
            HttpClient http = new HttpClient();
            InfoImgModel Pokemon = new InfoImgModel();
            var resp = await http.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                var respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<InfoPoke>(respString);
                Pokemon.Id = json.Id;
                orden = Pokemon.Id;
                return orden;   
            }
            return orden;
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
        private async void btPrev_Clicked(object sender, EventArgs e)
        {
            await GetPokemon(Previo);
            ListPoke.ScrollTo(1, ScrollToPosition.Start, false);
        }

        private async void btNext_Clicked(object sender, EventArgs e)
        {
            await GetPokemon(Siguiente);
            ListPoke.ScrollTo(1, ScrollToPosition.Start, false);
        }

        private async void ListPoke_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedItem = e.Item as PokeImgModel;
            string x = tappedItem.Name;
            await Navigation.PushAsync(new VistaPokemon(x));
        }

        //public Color BGColor(string color)
        //{
        //    Color resultado = new Color();
        //    switch (color)
        //    {
        //        case "red":
        //            resultado = Color.Red;
        //            break;
        //        case "blue":
        //            resultado = Color.Blue;
        //            break;
        //        case "yellow":
        //            resultado = Color.Yellow;
        //            break;
        //        case "green":
        //            resultado = Color.Green;
        //            break;
        //        case "black":
        //            resultado = Color.Black;
        //            break;
        //        case "brown":
        //            resultado = Color.Brown;
        //            break;
        //        case "purple":
        //            resultado = Color.Purple;
        //            break;
        //        case "gray":
        //            resultado = Color.Gray;
        //            break;
        //        case "white":
        //            resultado = Color.White;
        //            break;
        //        case "pink":
        //            resultado = Color.Pink;
        //            break;


        //    }
        //    return resultado;
        //}

    }
}