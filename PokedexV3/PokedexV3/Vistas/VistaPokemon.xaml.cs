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

namespace PokedexV3.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VistaPokemon : ContentPage
    {
        public string URL;
        public string UrlDesc;
        public string UrlEvo;
        private long Order;

        public VistaPokemon(string data)
        {
            InitializeComponent();
            URL = "https://pokeapi.co/api/v2/pokemon/" + data.ToLower();
            UrlDesc = "https://pokeapi.co/api/v2/pokemon-species/";
            UrlEvo = "https://pokeapi.co/api/v2/evolution-chain/";
            _ = GetInfo(URL);
            GridContenido.IsVisible = false;
            GridContenido.IsVisible = true;
        }
        public async Task<bool> GetInfo(string Url)
        {
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
                    var res = Traduccion(Pokemon.Types[i].Type.Name, "");
                    Button btn = new Button
                    {
                        Text = res,
                        IsEnabled = true,
                        CornerRadius = 50,
                        BackgroundColor = ColorTipo(Pokemon.Types[i].Type.Name),
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
                    };
                    if (json.Abilities[i].IsHidden)
                    {
                        btn.Text = btn.Text + " (Oculta)";
                    }

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

                //BackgroundColor = BGColor(PokeDetail.Color.Name);
                Contenido.BackgroundColor = BGColor(PokeDetail.Color.Name);
            }


            //GetEvoChain
            EvoModel evoModel = new EvoModel();
            resp = await http.GetAsync(UrlEvo);
            if (resp.IsSuccessStatusCode)
            {
                var respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<EvoModel>(respString);

                evoModel.Chain = json.Chain;
                Button btn = new Button
                {
                    Text = evoModel.Chain.Species.Name,
                    CornerRadius = 20,
                };
                grEvolution.RowDefinitions.Add(new RowDefinition());
                grEvolution.Children.Add(btn, 0, 1);
                if (evoModel.Chain.EvolvesTo.Length != 0)
                {
                    
                    Button btn2 = new Button
                    {
                        Text = evoModel.Chain.EvolvesTo[0].Species.Name,
                        CornerRadius = 20,
                    };
                    grEvolution.RowDefinitions.Add(new RowDefinition());
                    grEvolution.Children.Add(btn2,0, 2);
                }
                
                if (evoModel.Chain.EvolvesTo[0].EvolvesTo.Length != 0)
                {
                    Button btn3 = new Button
                    {
                        Text = evoModel.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name,
                        CornerRadius = 20,
                    };
                    grEvolution.RowDefinitions.Add(new RowDefinition());
                    grEvolution.Children.Add(btn3, 0, 3);
                }
                else
                {
                    btnTE.IsVisible = false;
                }
            }
            return true;
        }

        public async Task<string> GetFlavor(long order)
        {
            var Detalle = UrlDesc + order;
            HttpClient http = new HttpClient();
            DetalleModel PokeDetail = new DetalleModel();
            var resp = await http.GetAsync(Detalle);
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

                BackgroundColor = BGColor(PokeDetail.Color.Name);
                //Contenido.BackgroundColor = BGColor(PokeDetail.Color.Name);
            }

            return UrlEvo;
        }

        public async Task<bool> GetEvoChain(string Url)
        {
            HttpClient http = new HttpClient();
            EvoModel evoModel = new EvoModel();
            var resp = await http.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                var respString = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<EvoModel>(respString);

                evoModel.Chain = json.Chain;
                Button btn = new Button
                {
                    Text = evoModel.Chain.Species.Name,
                    CornerRadius = 20,
                };
                grEvolution.RowDefinitions.Add(new RowDefinition());
                grEvolution.Children.Add(btn, 0, 1);
                Button btn2 = new Button
                {
                    Text = evoModel.Chain.EvolvesTo[0].Species.Name,
                    CornerRadius = 20,
                };
                grEvolution.RowDefinitions.Add(new RowDefinition());
                grEvolution.Children.Add(btn2, 0, 2);
                Button btn3 = new Button
                {
                    Text = evoModel.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name,
                    CornerRadius = 20,
                };
                grEvolution.RowDefinitions.Add(new RowDefinition());
                grEvolution.Children.Add(btn3, 0, 3);
            }
            return true;
        }
        public string Traduccion(string tipo, string resultado)
        {

            switch (tipo)
            {
                case "bug":
                    resultado = "INSECTO";
                    break;
                case "dark":
                    resultado = "Oscuro";
                    break;
                case "dragon":
                    resultado = "dragon";
                    break;
                case "electric":
                    resultado = "electrico";
                    break;
                case "fairy":
                    resultado = "Hada";
                    break;
                case "fighting":
                    resultado = "Pelea";
                    break;
                case "fire":
                    resultado = "Fuego";
                    break;
                case "flying":
                    resultado = "Volador";
                    break;
                case "ghost":
                    resultado = "Fantasma";
                    break;
                case "grass":
                    resultado = "pasto";
                    break;
                case "ground":
                    resultado = "Tierra";
                    break;
                case "ice":
                    resultado = "Hielo";
                    break;
                case "normal":
                    resultado = "normal";
                    break;
                case "poison":
                    resultado = "Veneno";
                    break;
                case "psychic":
                    resultado = "Psiquico";
                    break;
                case "rock":
                    resultado = "Roca";
                    break;
                case "steel":
                    resultado = "Acero";
                    break;
                case "water":
                    resultado = "Agua";
                    break;

            }
            return resultado;
        }

        public Color ColorTipo(string color)
        {
            Color resultado = new Color();
            switch (color)
            {
                case "bug":
                    resultado = Color.FromRgb(26, 153, 79);
                    break;
                case "dark":
                    resultado = Color.FromRgb(89, 90, 121);
                    break;
                case "dragon":
                    resultado = Color.FromRgb(42, 203, 218);
                    break;
                case "electric":
                    resultado = Color.FromRgb(249, 249, 117);
                    break;
                case "fairy":
                    resultado = Color.FromRgb(251, 26, 105);
                    break;
                case "fighting":
                    resultado = Color.FromRgb(251, 99, 57);
                    break;
                case "fire":
                    resultado = Color.FromRgb(251, 77, 91);
                    break;
                case "flying":
                    resultado = Color.FromRgb(137, 179, 200);
                    break;
                case "ghost":
                    resultado = Color.FromRgb(157, 104, 146);
                    break;
                case "grass":
                    resultado = Color.FromRgb(38, 202, 81);
                    break;
                case "ground":
                    resultado = Color.FromRgb(120, 71, 27);
                    break;
                case "ice":
                    resultado = Color.FromRgb(210, 241, 250);
                    break;
                case "normal":
                    resultado = Color.FromRgb(217, 154, 168);
                    break;
                case "poison":
                    resultado = Color.FromRgb(171, 108, 218);
                    break;
                case "psychic":
                    resultado = Color.FromRgb(251, 36, 147);
                    break;
                case "rock":
                    resultado = Color.FromRgb(158, 61, 30);
                    break;
                case "steel":
                    resultado = Color.FromRgb(36, 189, 149);
                    break;
                case "water":
                    resultado = Color.FromRgb(20, 171, 251);
                    break;

            }
            return resultado;
        }

        public Color BGColor(string color)
        {
            Color resultado = new Color();
            switch (color)
            {
                case "red":
                    resultado = Color.FromRgb(255, 153, 154);
                    break;
                case "blue":
                    resultado = Color.FromRgb(209, 217, 237);
                    break;
                case "yellow":
                    resultado = Color.FromRgb(255, 255, 205);
                    break;
                case "green":
                    resultado = Color.FromRgb(203, 246, 204);
                    break;
                case "black":
                    resultado = Color.FromRgb(51, 51, 47);
                    break;
                case "brown":
                    resultado = Color.FromRgb(188, 146, 114);
                    break;
                case "purple":
                    resultado = Color.FromRgb(198, 158, 221);
                    break;
                case "gray":
                    resultado = Color.FromRgb(165, 165, 152);
                    break;
                case "white":
                    resultado = Color.FromRgb(237, 255, 234);
                    break;
                case "pink":
                    resultado = Color.FromRgb(246, 204, 227);
                    break;


            }
            return resultado;
        }

    }
}