using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet.Foods
{
    internal class Food
    {
        public string Name { get; protected set; }
        public int XpValue => IsFavorite ? 50 : 20;
        public string Description { get; protected set; }
        public bool IsFavorite { get; private set; }
        public int hungerValue => IsFavorite ? 50 : 25;
        public string SetFavorite()
        {
            IsFavorite = true;
            return Name;
        }

        public Food()
        {
            Name = string.Empty;


        }

    }
    
}
