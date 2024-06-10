using Game_Hall._Domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>()
                .Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired(true);


            modelBuilder.Entity<Player>()
                .Property(p => p.id)
                .HasColumnType("smallint")
                .IsRequired(true);

            modelBuilder.Entity<Player>()
                .Property(p => p.phone)
                .HasColumnType("smallint");





            modelBuilder.Entity<Game>()
                .Property(G => G.name)
                .HasMaxLength(200);


            modelBuilder.Entity<Game>()
                .Property(G => G.id)
                .HasColumnType("smallint")
                .IsRequired(true);


            modelBuilder.Entity<Game>()
                .Property(G => G.tybe)
                .HasMaxLength(20)
                 .IsRequired(true);



        }

    }
}
