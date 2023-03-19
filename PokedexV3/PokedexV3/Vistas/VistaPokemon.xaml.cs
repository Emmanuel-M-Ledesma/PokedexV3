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
using System.Runtime.Serialization;
using Acr.UserDialogs;
using System.ComponentModel.Design;
using PokedexV3.Resources;

namespace PokedexV3.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VistaPokemon : ContentPage
    {
        public string URL;
        public string UrlDesc;
        public string UrlEvo;
        private string flavor;

        public VistaPokemon(string Nombre, string url)
        {
            InitializeComponent();
            URL = "https://pokeapi.co/api/v2/pokemon/";
            UrlDesc = url.ToLower();
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

        public async Task<bool> GetInfo(string Url)
        {
            //GetFlavor
            Enums Enumeraciones = new Enums();
            HttpClient http = new HttpClient();
            DetalleModel PokeDetail = new DetalleModel();
            HttpResponseMessage resp = await http.GetAsync(UrlDesc);
            if (resp.IsSuccessStatusCode)
            {
                string respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<DetalleModel>(respString);
                PokeDetail = json;
                UrlEvo = json.EvolutionChain.Url.ToString();


                PokeDetail.FlavorTextEntries = json.FlavorTextEntries;


                var desc = PokeDetail.FlavorTextEntries
                .FirstOrDefault(x => x.Language.Name == "es")
                ?.FlavorText.Replace("\n", " ");

                if (!string.IsNullOrWhiteSpace(desc))
                {
                    lblDesc.Text = desc;
                    lblDesc.FontSize = 15;
                }
                else
                {
                    lblDesc.Text = PokeDetail.FlavorTextEntries[0].FlavorText.Replace("\n", " ");

                    lblDesc.FontSize = 15;
                }

                Contenido.BackgroundColor = Enumeraciones.BGColor(PokeDetail.Color.Name);
            }

            string Gpk = URL + PokeDetail.Id;
            InfoImgModel Pokemon = new InfoImgModel();
            resp = await http.GetAsync(Gpk);
            if (resp.IsSuccessStatusCode)
            {
                var respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<InfoPoke>(respString);


                Pokemon.UrlImg = "https://img.pokemondb.net/sprites/home/normal/" + PokeDetail.Name + ".png";
                Pokemon.Name = PokeDetail.Name;
                Pokemon.Height = json.Height;
                Pokemon.Weight = json.Weight;
                Pokemon.Types = json.Types;

                for (int i = 0; i < json.Types.Length; i++)
                {
                    var res = Enumeraciones.Tipos(json.Types[i].Type.Name);
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

                if (Pokemon.Weight > 10)
                {
                    lblWeight.Text = Pokemon.Weight.ToString().Insert(Pokemon.Weight.ToString().Count() - 1, ",") + " Kg.";
                }
                else
                {
                    lblWeight.Text = Pokemon.Weight.ToString() + "00 Gr";
                }
                lblHeight.Text = Pokemon.Height.ToString() + "0 cm.";
                lblName.Text = Pokemon.Name.ToUpper();
                ImgPoke.Source = Pokemon.UrlImg;

            }


            //GetEvoChain
            EvoModel evoModel = new EvoModel();
            resp = await http.GetAsync(UrlEvo);
            if (resp.IsSuccessStatusCode)
            {
                var respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<EvoModel>(respString);

                evoModel.Chain = json.Chain;

                txtPE.Text = evoModel.Chain.Species.Name;
                ImgPE.Source = "https://img.pokemondb.net/sprites/home/normal/" + evoModel.Chain.Species.Name + ".png";
                ImgPE.CommandParameter = evoModel.Chain.Species.Url;


                if (evoModel.Chain.EvolvesTo.Length != 0)
                {
                    if (Pokemon.Name == evoModel.Chain.Species.Name)
                    {
                        frPE.BackgroundColor = Contenido.BackgroundColor;
                        ImgPE.BackgroundColor = Contenido.BackgroundColor;
                    }

                    if (evoModel.Chain.Species.Name == "eevee")
                    {
                        for (int i = 0; i < evoModel.Chain.EvolvesTo.Length; i++)
                        {
                            if (evoModel.Chain.EvolvesTo[i].Species.Name == Pokemon.Name)
                            {
                                frSE.BackgroundColor = Contenido.BackgroundColor;
                                ImgSE.BackgroundColor = Contenido.BackgroundColor;
                                txtSE.Text = evoModel.Chain.EvolvesTo[i].Species.Name;
                                ImgSE.Source = "https://img.pokemondb.net/sprites/home/normal/" + evoModel.Chain.EvolvesTo[i].Species.Name + ".png";
                                ImgSE.CommandParameter = evoModel.Chain.EvolvesTo[i].Species.Url;
                            }
                        }
                    }
                    else
                    {
                        if (Pokemon.Name == evoModel.Chain.EvolvesTo[0].Species.Name)
                        {
                            frSE.BackgroundColor = Contenido.BackgroundColor;
                            ImgSE.BackgroundColor = Contenido.BackgroundColor;
                        }
                        txtSE.Text = evoModel.Chain.EvolvesTo[0].Species.Name;
                        ImgSE.Source = "https://img.pokemondb.net/sprites/home/normal/" + evoModel.Chain.EvolvesTo[0].Species.Name + ".png";
                        ImgSE.CommandParameter = evoModel.Chain.EvolvesTo[0].Species.Url;
                    }

                    if (evoModel.Chain.EvolvesTo[0].EvolvesTo.Length != 0)
                    {
                        string url = evoModel.Chain.EvolvesTo[0].EvolvesTo[0].Species.Url.ToString();
                        string nombre = evoModel.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name;
                        string url2 = "", nombre2 = "", nombre3;
                        if (evoModel.Chain.EvolvesTo[0].EvolvesTo.Length > 1)
                        {
                            lblTP2.IsVisible = true;
                            for (int i = 0; i < evoModel.Chain.EvolvesTo[0].EvolvesTo.Length; i++)
                            {
                                nombre3 = "vacio";
                                if (evoModel.Chain.EvolvesTo[0].EvolvesTo[i].Species.Name == Pokemon.Name)
                                {

                                    frTE.BackgroundColor = Contenido.BackgroundColor;
                                    ImgTE.BackgroundColor = Contenido.BackgroundColor;
                                    nombre = evoModel.Chain.EvolvesTo[0].EvolvesTo[i].Species.Name;
                                    nombre3 = evoModel.Chain.EvolvesTo[0].EvolvesTo[i].Species.Name;
                                    url = evoModel.Chain.EvolvesTo[0].EvolvesTo[i].Species.Url.ToString();
                                    grEvolution.Children.Remove(frTPE);
                                    grEvolution.Children.Remove(txtTPE);
                                    grEvolution.Children.Remove(lblTP2);

                                }
                                if (i == 0 && nombre3 == "vacio")
                                {
                                    nombre = evoModel.Chain.EvolvesTo[0].EvolvesTo[i].Species.Name;
                                    url = evoModel.Chain.EvolvesTo[0].EvolvesTo[i].Species.Url.ToString();

                                }
                                if (i == 1 && nombre3 == "vacio")
                                {
                                    nombre2 = evoModel.Chain.EvolvesTo[0].EvolvesTo[i].Species.Name;
                                    url2 = evoModel.Chain.EvolvesTo[0].EvolvesTo[i].Species.Url.ToString();

                                }

                            }
                        }
                        else if (Pokemon.Name == evoModel.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name)
                        {
                            frTE.BackgroundColor = Contenido.BackgroundColor;
                            ImgTE.BackgroundColor = Contenido.BackgroundColor;
                        }
                        if (nombre2 != "" && url2 != "")
                        {
                            txtTPE.Text = nombre2;
                            ImgTPE.Source = "https://img.pokemondb.net/sprites/home/normal/" + nombre2 + ".png";
                            ImgTPE.CommandParameter = url2;
                        }
                        else
                        {
                            grEvolution.Children.Remove(frTPE);
                            grEvolution.Children.Remove(txtTPE);
                        }
                        txtTE.Text = nombre;
                        ImgTE.Source = "https://img.pokemondb.net/sprites/home/normal/" + nombre + ".png";
                        ImgTE.CommandParameter = url;

                    }
                    else
                    {
                        frTE.IsVisible = false;
                        txtTE.Text = string.Empty;
                        lblT2.Text = string.Empty;
                        grEvolution.Children.Remove(frTPE);
                        grEvolution.Children.Remove(txtTPE);
                    }
                }
                else
                {
                    txtSE.Text = string.Empty;
                    txtTE.Text = string.Empty;
                    lblT1.Text = string.Empty;
                    lblT2.Text = string.Empty;
                    frSE.IsVisible = false;
                    frTE.IsVisible = false;
                    grEvolution.Children.Remove(frTPE);
                    grEvolution.Children.Remove(txtTPE);
                }

            }
            return true;
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

        private async void ImgPE_Clicked(object sender, EventArgs e)
        {
            if (frPE.BackgroundColor == Color.Gray)
            {
                if (txtPE.Text == "eevee")
                {
                    await Navigation.PushAsync(new VistaEevee(txtPE.Text));
                }
                else
                {
                    await Navigation.PushAsync(new VistaPokemon(txtPE.Text, ImgPE.CommandParameter.ToString()));
                }
            }
        }

        private async void ImgSE_Clicked(object sender, EventArgs e)
        {
            if (frSE.BackgroundColor == Color.Gray)
            {

                await Navigation.PushAsync(new VistaPokemon(txtSE.Text, ImgSE.CommandParameter.ToString()));
            }
        }

        private async void ImgTE_Clicked(object sender, EventArgs e)
        {
            if (frTE.BackgroundColor == Color.Gray)
            {
                await Navigation.PushAsync(new VistaPokemon(txtTE.Text, ImgTE.CommandParameter.ToString()));
            }
        }

        private async void HandlerComun(object sender, EventArgs e)
        {
            var habilidad = ((Button)sender).Text.ToUpper();
            string url = ((Button)sender).CommandParameter.ToString();
            var texto = await InfoHability(url);

            await DisplayAlert(habilidad, texto, "Cerrar");
        }

        private async void ImgTPE_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VistaPokemon(txtTPE.Text, ImgTPE.CommandParameter.ToString()));

        }

    }
}