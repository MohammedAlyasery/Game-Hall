using Game_Hall._Domain.entities;


namespace GameHall.Servicesinterfaces
{
    public interface IPlayerServices
    {
        Task Delete(Player player);
        Task<Player> Get(int id);
        Task<List<Player>> GetList(string name);
        Task<List<Player>> GetAll();
        Task Save (Player player);
        Task Update(Player player);


    }
}
