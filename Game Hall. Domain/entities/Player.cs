﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hall._Domain.entities
{
    public class Player
    {

        public int id { get; set; }
        public string Name { get; set; }

        public int phone { get; set; }

        public List<Game> Games { get; set; }
     


        //public int GameId { get; set; }
      //public Game Game { get; set; }
    }
}
