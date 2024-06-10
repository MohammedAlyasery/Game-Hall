using Game_Hall.Persistance;
using Game_Hall._Domain.entities;
using Microsoft.EntityFrameworkCore;
using GameHall.Servicesinterfaces;

namespace GameHall.Services
{
    public class GameServices : IGameServices
    {
         


        private readonly IDbContextFactory<Game_Hall_Context> _contextFactory;

        public GameServices(IDbContextFactory<Game_Hall_Context> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        public async Task Save(Game game)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.games.FirstOrDefault(x => x.id == game.id);

            if (tmp == null)
            {
                db.games.Add(game);
                await db.SaveChangesAsync();
            }

        }

        public async Task Update(Game game)
        {
            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.games.FirstOrDefault(x => x.id == game.id);

            if (tmp != null)
            {
                tmp.name = game.name;
                tmp.tybe = game.tybe;
                

                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(Game game)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.games.FirstOrDefault(x => x.id == game.id);

            if (tmp != null)
            {
                db.games.Remove(tmp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Game> Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var game = await db.games.FirstOrDefaultAsync(x => x.id == id);
            return game;
        }

        public async Task<Game> Get(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var game = await db.games.FirstOrDefaultAsync(x => x.name.ToUpper() == name.Trim().ToUpper());
            return game;
        }

        public async Task<List<Game>> GetList(string tybe)
        {
            using var db = _contextFactory.CreateDbContext();

            var game = await db.games.Where(x => x.tybe.Contains(tybe)).ToListAsync();
            return [.. game];
        }

        public async Task<List<Game>> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return [.. await db.games.Include(x => x.Players).ToListAsync()];
        }

        public async Task AddPlayerToGame(Game game, Player player)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmpBook = db.games.Include(x => x.Players).FirstOrDefault(x => x.id == game.id);
            if (tmpBook != null)
            {
                var tmpAuthor = db.Players.FirstOrDefault(x => x.id == player.id);
                if (tmpAuthor != null)
                {
                    tmpBook.Players.Add(tmpAuthor);
                }
                else
                {
                    db.Players.Add(player);
                    await db.SaveChangesAsync();
                    tmpBook.Players.Add(player);
                }
                await db.SaveChangesAsync();
            }
        }

        public async Task RemovePlayerFromGame(Game game, Player player)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmpBook = db.games.Include(x => x.Players).FirstOrDefault(x => x.id == game.id);
            if (tmpBook != null)
            {
                var bookAuthor = tmpBook.Players.FirstOrDefault(x => x.id == player.id);
                if (bookAuthor != null)
                {
                    tmpBook.Players.Remove(bookAuthor);
                    await db.SaveChangesAsync();
                }
            }
        }
    }


}

