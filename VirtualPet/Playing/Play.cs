using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet.Playing
{
    internal class Play
    {
        public string Name { get; protected set; }
        public int XpValue => IsFavorite ? 50 : 20;
        public string Description { get; protected set; }
        public bool IsFavorite { get; private set; }
        public int HappinessValue => IsFavorite ? 50 : 25;
        public string SetFavorite()
        {
            IsFavorite = true;
            return Name;
        }

        public Play()
        {
            Name = string.Empty;


        }






    }
}
