using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PokedexV3.Resources
{
    public class Enums
    {

        public string Tipos(string key)
        {
            Dictionary<string, string> tipos = new Dictionary<string, string>()
            {
                {"bug", "INSECTO" },
                {"dark", "Oscuro" },
                {"dragon", "dragon" },
                {"electric","electrico" },
                {"fairy","hada" },
                {"fighting","pelea" },
                {"fire","fuego" },
                {"flying","volador" },
                {"ghost", "fantasma" },
                {"grass","pasto" },
                {"ground","tierra" },
                {"ice", "hielo"},
                {"normal","normal" },
                {"poison","veneno" },
                {"psychic", "psiquico" },
                {"rock", "roca"},
                {"steel","acero"},
                {"water","agua" }

            };

            return tipos[key].ToUpper();
        }

        public Color BGColor(string key)
        {
            Dictionary<string, Color> colores = new Dictionary<string, Color>()
            {
                {"red", Color.FromRgb(255, 153, 154)},
                {"blue", Color.FromRgb(209, 217, 237)},
                {"yellow", Color.FromRgb(255, 255, 205)},
                {"green", Color.FromRgb(203, 246, 204)},
                {"black", Color.FromRgb(51, 51, 47)},
                {"brown", Color.FromRgb(188, 146, 114)},
                {"purple", Color.FromRgb(198, 158, 221)},
                {"gray", Color.FromRgb(165, 165, 152)},
                {"white", Color.FromRgb(237, 255, 234)},
                {"pink", Color.FromRgb(246, 204, 227)}
            };

            return colores[key];
        }

        public Color ColorTipo(string key)
        {
            Dictionary<string, Color> colores = new Dictionary<string, Color>()
            {
                {"bug", Color.FromRgb(26, 153, 79)},
                {"dark", Color.FromRgb(89, 90, 121)},
                {"dragon", Color.FromRgb(42, 203, 218)},
                {"electric", Color.FromRgb(249, 249, 117)},
                {"fairy", Color.FromRgb(251, 26, 105)},
                {"fighting", Color.FromRgb(251, 99, 57)},
                {"fire", Color.FromRgb(251, 77, 91)},
                {"flying", Color.FromRgb(137, 179, 200)},
                {"ghost", Color.FromRgb(157, 104, 146)},
                {"grass", Color.FromRgb(38, 202, 81)},
                {"ground", Color.FromRgb(120, 71, 27)},
                {"ice", Color.FromRgb(210, 241, 250)},
                {"normal", Color.FromRgb(217, 154, 168)},
                {"poison", Color.FromRgb(171, 108, 218)},
                {"psychic", Color.FromRgb(251, 36, 147)},
                {"rock", Color.FromRgb(158, 61, 30)},
                {"steel", Color.FromRgb(36, 189, 149)},
                {"water", Color.FromRgb(20, 171, 251)}
            };

            return colores[key];
        }


    }
}
