using Game_Hall._Domain.entities;

namespace GameHall.Servicesinterfaces
{
    public interface IGameServices
    {
        Task Delete(Game game);

        Task<Game> Get(int id);

        Task<Game> Get(string name);

        Task<List<Game>> GetList(string tybe);

        Task Save(Game game);

        Task Update(Game game);

        Task<List<Game>> GetAll();

        Task AddPlayerToGame(Game game, Player player);
        Task RemovePlayerFromGame(Game game, Player player);




    }
}
