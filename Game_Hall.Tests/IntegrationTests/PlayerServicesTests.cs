using Game_Hall._Domain.entities;
using Game_Hall.Persistance;
using GameHall.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Hall.Tests.IntegrationTests
{
    public class PlayerServicesTests
    {



        private DbContextOptions<Game_Hall_Context> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<Game_Hall_Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private IDbContextFactory<Game_Hall_Context> GetDbContextFactoryAsync(DbContextOptions<Game_Hall_Context> options)
        {
            var mockDbFactory = new Mock<IDbContextFactory<Game_Hall_Context>>();
            mockDbFactory.Setup(f => f.CreateDbContext()).Returns(() => new Game_Hall_Context(options));
            return mockDbFactory.Object;

        }

        [Fact]
        public async Task Save_ShouldAddPlayer()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerServices(factory);
            var player = new Player { Name = "Player1", phone = 1 };

            // Act
            await service.Save(player);

            // Assert
            using var context = new Game_Hall_Context(options);
            var savedPlayer = await context.Players.FirstOrDefaultAsync(a => a.Name == "Player1");
            Assert.NotNull(savedPlayer);
        }

        [Fact]
        public async Task Get_ShouldReturnPlayerById()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerServices (factory);
            var player = new Player { Name = "Player1", phone =1 };
            await service.Save(player);

            // Act
            var fetchedPlayer = await service.Get(player.id);

            // Assert
            Assert.NotNull(fetchedPlayer);
            Assert.Equal(player.Name, fetchedPlayer.Name);
        }

        [Fact]
        public async Task GetList_ShouldReturnPlayersByName()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerServices(factory);
            await service.Save(new Player { Name = "Player1", phone = 1 });
            await service.Save(new Player { Name = "Player2", phone = 1 });

            // Act
            var players = await service.GetList("Player");

            // Assert
            Assert.Equal(2, players.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllPlayers()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerServices(factory);
            await service.Save(new Player { Name = "Player1", phone = 1  });
            await service.Save(new Player { Name = "Player2", phone = 1 });

            // Act
            var players = await service.GetAll();

            // Assert
            Assert.Equal(2, players.Count);
        }

        [Fact]
        public async Task Delete_ShouldRemovePlayer()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerServices(factory);
            var player = new Player { Name = "Player1", phone = 1 };
            await service.Save(player);

            // Act
            await service.Delete(player);

            // Assert
            using var context = new Game_Hall_Context(options);
            var deletedPlayer = await context.Players.FindAsync(player.id);
            Assert.Null(deletedPlayer);
        }

        [Fact]
        public async Task Update_ShouldModifyPlayer()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerServices(factory);
            var player = new Player { Name = "Player1", phone =1 };
            await service.Save(player);

            // Act
            player.Name = "Updated Player";
           
            player.phone = 0987654321;
            await service.Update(player);

            // Assert
            using var context = new Game_Hall_Context(options);
            var updatedPlayer = await context.Players.FindAsync(player.id);
            Assert.Equal("Updated Player", updatedPlayer.Name);
         
            Assert.Equal(0987654321, updatedPlayer.phone);
        }


    }



}
