using Newtonsoft.Json;
using PokedexV3.Models.Info;
using PokedexV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using PokedexV3.Resources;
using Xamarin.Essentials;

namespace PokedexV3.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VistaEevee : ContentPage
    {
        public string URL;
        public string UrlDesc;
        public string UrlEvo;
        private long Order;
        private string flavor;

        public VistaEevee(string data)
        {
            InitializeComponent();
            URL = "https://pokeapi.co/api/v2/pokemon/" + data.ToLower();
            UrlDesc = "https://pokeapi.co/api/v2/pokemon-species/";
            UrlEvo = "https://pokeapi.co/api/v2/evolution-chain/";


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
            await GetInfo(URL);
            GridContenido.IsVisible = false;
            GridContenido.IsVisible = true;
        }

        private async Task CheckConnAsync(VistaPokemon vista)
        {
            // Intenta establecer la conexión de nuevo aquí
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                // Hay conexión a Internet, cambia a la vista normal
                await Navigation.PushAsync(vista);

            }
            else
            {
                await Navigation.PushAsync(new NoConexion());
            }
        }

        

        public async Task<bool> GetInfo(string Url)
        {
            Enums Enumeraciones = new Enums();
            HttpClient http = new HttpClient();
            InfoImgModel Pokemon = new InfoImgModel();
            var resp = await http.GetAsync(URL);
            if (resp.IsSuccessStatusCode)
            {
                var respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<InfoPoke>(respString);


                Pokemon.UrlImg = "https://img.pokemondb.net/sprites/home/normal/" + json.Name + ".png";
                Pokemon.Name = json.Name;
                Pokemon.Height = json.Height;
                Pokemon.Weight = json.Weight;
                Pokemon.Types = json.Types;

                for (int i = 0; i < json.Types.Length; i++)
                {
                    var res = Enumeraciones.Tipos(Pokemon.Types[i].Type.Name);
                    Button btn = new Button
                    {
                        Text = res,
                        IsEnabled = true,
                        CornerRadius = 50,
                        BackgroundColor = Enumeraciones.ColorTipo(Pokemon.Types[i].Type.Name),
                        TextColor = Color.FromRgb(13, 13, 12),
                        BorderColor = Color.Black,
                    };
                    gridtipo.ColumnDefinitions.Add(new ColumnDefinition());
                    gridtipo.Children.Add(btn, i, 0);

                }
                for (int i = 0; i < json.Abilities.Length; i++)
                {
                    Button btn = new Button
                    {
                        Text = json.Abilities[i].AbilityAbility.Name,
                        CornerRadius = 20,
                        BorderColor = Color.Black,
                        CommandParameter = json.Abilities[i].AbilityAbility.Url,
                        
                    };
                    if (json.Abilities[i].IsHidden)
                    {
                        btn.Text = btn.Text + " (Oculta)";
                    }
                    btn.Clicked += HandlerComun;

                    grHability.RowDefinitions.Add(new RowDefinition());
                    grHability.Children.Add(btn, 0, i + 1);
                }

                lblWeight.Text = Pokemon.Weight.ToString().Insert(Pokemon.Weight.ToString().Count() - 1, ",") + " Kg.";
                lblHeight.Text = Pokemon.Height.ToString() + "0 cm.";
                lblName.Text = Pokemon.Name.ToUpper();
                ImgPoke.Source = Pokemon.UrlImg;
                Order = json.Id;


            }

            //GetFlavor

            var Detalle = UrlDesc + Order;
            DetalleModel PokeDetail = new DetalleModel();
            resp = await http.GetAsync(Detalle);
            if (resp.IsSuccessStatusCode)
            {
                var respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<DetalleModel>(respString);
                PokeDetail = json;
                UrlEvo = json.EvolutionChain.Url.ToString();


                PokeDetail.FlavorTextEntries = json.FlavorTextEntries;
                for (int i = 0; i < PokeDetail.FlavorTextEntries.Length; i++)
                {
                    if (PokeDetail.FlavorTextEntries[i].Language.Name == "es")
                    {
                        lblDesc.Text = PokeDetail.FlavorTextEntries[i].FlavorText.Replace("\n", " ");
                        i = PokeDetail.FlavorTextEntries.Length;
                        lblDesc.FontSize = 15;
                    }

                }
                if (lblDesc.Text == "")
                {
                    lblDesc.Text = PokeDetail.FlavorTextEntries[0].FlavorText.Replace("\n", " ");

                    lblDesc.FontSize = 15;
                }

                Contenido.BackgroundColor = Enumeraciones.BGColor(PokeDetail.Color.Name);
            }


            //GetEvoChain
            EvoModel evoModel = new EvoModel();
            resp = await http.GetAsync(UrlEvo);
            if (resp.IsSuccessStatusCode)
            {
                var respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<EvoModel>(respString);

                evoModel.Chain = json.Chain;

                txtNor.Text = evoModel.Chain.Species.Name;
                ImgNor.Source = "https://img.pokemondb.net/sprites/home/normal/" + evoModel.Chain.Species.Name + ".png";
                frNorm.BackgroundColor = Contenido.BackgroundColor;
                ImgNor.BackgroundColor = Contenido.BackgroundColor;

                await DownloadImage(txtPPE, ImgPPE, evoModel.Chain.EvolvesTo[0].Species.Name);
                await DownloadImage(txtSPE, ImgSPE, evoModel.Chain.EvolvesTo[1].Species.Name);
                await DownloadImage(txtTPE, ImgTPE, evoModel.Chain.EvolvesTo[2].Species.Name);
                await DownloadImage(txtCPE, ImgCPE, evoModel.Chain.EvolvesTo[3].Species.Name);
                await DownloadImage(txtQPE, ImgQPE, evoModel.Chain.EvolvesTo[4].Species.Name);
                await DownloadImage(txtSePE, ImgSePE, evoModel.Chain.EvolvesTo[5].Species.Name);
                await DownloadImage(txtSepPE, ImgSepPE, evoModel.Chain.EvolvesTo[6].Species.Name);
                await DownloadImage(txtOPE, ImgOPE, evoModel.Chain.EvolvesTo[7].Species.Name);

            }
            return true;
        }


        async Task DownloadImage(Label label, ImageButton image, string name)
        {
            label.Text = name;
            var imageUrl = "https://img.pokemondb.net/sprites/home/normal/" + name + ".png";
            var imageData = await new HttpClient().GetByteArrayAsync(imageUrl);
            image.Source = ImageSource.FromStream(() => new MemoryStream(imageData));
            image.CommandParameter = imageUrl;
        }


        public async Task<string> InfoHability(string url)
        {
            HttpClient http = new HttpClient();
            var respuesta = await http.GetAsync(url);
            if (respuesta.IsSuccessStatusCode)
            {
                var respString = await respuesta.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<AbilityModel>(respString);

                for (int i = 0; i < json.FlavorTextEntries.Length; i++)
                {
                    if (json.FlavorTextEntries[i].Language.Name == "es")
                    {
                        flavor = json.FlavorTextEntries[i].FlavorText.Replace("\n", " ");
                    }
                }
            }

            return flavor;
        }
        

        private async void ImgPPE_Clicked(object sender, EventArgs e)
        {
            await CheckConnAsync(new VistaPokemon(txtPPE.Text, ImgPPE.CommandParameter.ToString()));            
        }

        private async void ImgSPE_Clicked(object sender, EventArgs e)
        {
            await CheckConnAsync(new VistaPokemon(txtSPE.Text,ImgSPE.CommandParameter.ToString()));
        }

        private async void ImgTPE_Clicked(object sender, EventArgs e)
        {
            await CheckConnAsync(new VistaPokemon(txtTPE.Text, ImgTPE.CommandParameter.ToString()));
        }

        private async void ImgCPE_Clicked(object sender, EventArgs e)
        {
            await CheckConnAsync(new VistaPokemon(txtCPE.Text, ImgCPE.CommandParameter.ToString()));
        }

        private async void ImgQPE_Clicked(object sender, EventArgs e)
        {
            await CheckConnAsync(new VistaPokemon(txtQPE.Text, ImgQPE.CommandParameter.ToString()));
        }

        private async void ImgSePE_Clicked(object sender, EventArgs e)
        {
            await CheckConnAsync(new VistaPokemon(txtSePE.Text, ImgSePE.CommandParameter.ToString()));
        }

        private async void ImgSepPE_Clicked(object sender, EventArgs e)
        {
            await CheckConnAsync(new VistaPokemon(txtSepPE.Text, ImgSepPE.CommandParameter.ToString()));
        }

        private async void ImgOPE_Clicked(object sender, EventArgs e)
        {
            await CheckConnAsync(new VistaPokemon(txtOPE.Text, ImgOPE.CommandParameter.ToString()));
        }
        private async void HandlerComun(object sender, EventArgs e)
        {
            var habilidad = ((Button)sender).Text.ToUpper();
            string url = ((Button)sender).CommandParameter.ToString();
            var texto = await InfoHability(url);

            await DisplayAlert(habilidad, texto, "Cerrar");
        }
    }

}