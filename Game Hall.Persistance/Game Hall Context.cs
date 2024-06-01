using Game_Hall._Domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hall.Persistance
{
    public class Game_Hall_Context : DbContext
    {

        public Game_Hall_Context(DbContextOptions<Game_Hall_Context> options) : base(options) 
        {
        
        }







        public DbSet<Player> Players { get; set; }
        
        public DbSet<Game> games { get; set; }
    }
}
