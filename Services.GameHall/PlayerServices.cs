
using Game_Hall.Persistance;
using Game_Hall._Domain.entities;
using Microsoft.EntityFrameworkCore;
using GameHall.Servicesinterfaces;



namespace GameHall.Services
{
    public class PlayerServices: IPlayerServices
    {

        
            private readonly IDbContextFactory<Game_Hall_Context> _contextFactory;

        public PlayerServices(IDbContextFactory<Game_Hall_Context> contextFactory)
            {
                _contextFactory = contextFactory;
            }


            public async Task<Player> Get(int id)
            {
                using var db = _contextFactory.CreateDbContext();

                var player = await db.Players.FirstOrDefaultAsync(x => x.id == id);
                return player;
            }

            public async Task<List<Player>> GetList(string name)
            {
                using var db = _contextFactory.CreateDbContext();

                var authors = db.Players.Where(x => x.Name.Contains(name));
                return [.. await authors.ToListAsync()];
            }

            public async Task Save(Player player)
            {
                using var db = _contextFactory.CreateDbContext();

                var tmp = db.Players.FirstOrDefault(x => x.id == player.id);

                if (tmp == null)
                {
                    db.Players.Add(player);
                    await db.SaveChangesAsync();
                }
            }
            public async Task Update(Player player)
            {
                using var db = _contextFactory.CreateDbContext();

                var tmp = db.Players.FirstOrDefault(y => y.id == player.id);

                if (tmp != null)
                {
 
                    tmp.phone = player.phone;
                    tmp.Name = player.Name;

                    await db.SaveChangesAsync();
                }
            }
            public async Task Delete(Player player)
            {
                using var db = _contextFactory.CreateDbContext();

                var tmp = db.Players.FirstOrDefault(x => x.id == player.id);
                if (tmp != null)
                {
                    db.Players.Remove(tmp);
                    await db.SaveChangesAsync();
                }
            }

               public async Task<List<Player>> GetAll()
            {
                using var db = _contextFactory.CreateDbContext();

                return await db.Players.ToListAsync();
            }
        }




    }

