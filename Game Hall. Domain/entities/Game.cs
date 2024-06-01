using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hall._Domain.entities
{
    public class Game
    {
        public int id { get; set; }
        public string name { get; set; }

        public string tybe { get; set; }

        public List<Player> Players { get; set; }

    }
}
