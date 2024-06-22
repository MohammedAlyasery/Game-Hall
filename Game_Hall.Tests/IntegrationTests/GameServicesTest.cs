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
    public class GameServicesTest
    {
            private DbContextOptions<Game_Hall_Context> CreateNewContextOptions()
            {
                return new DbContextOptionsBuilder<Game_Hall_Context>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            }

            private IDbContextFactory<Game_Hall_Context> GetDbContextFactory(DbContextOptions<Game_Hall_Context> options)
            {
                var mockFactory = new Mock<IDbContextFactory<Game_Hall_Context>>();
                mockFactory.Setup(f => f.CreateDbContext()).Returns(() => new Game_Hall_Context(options));
                return mockFactory.Object;
            }

            [Fact]
            public async Task Save_ShouldAddGame()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactory(options);
                var service = new GameServices(factory);
                var game = new Game { name = "Game1", tybe = "fifa" };

                // Act
                await service.Save(game);

                // Assert
                using var context = new Game_Hall_Context(options);
                var savedGame = await context.games.FirstOrDefaultAsync(b => b.name == "Game1");
                Assert.NotNull(savedGame);
            }

            [Fact]
            public async Task GetById_ShouldReturnGameId()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactory(options);
                var service = new GameServices(factory);
                var game = new Game { name = "Game1", tybe = "fifa" };
                await service.Save(game);

                // Act
                var fetchedGame = await service.Get(game.id);

                // Assert
                Assert.NotNull(fetchedGame);
                Assert.Equal(game.name, fetchedGame.name);
            }

            [Fact]
            public async Task GetByTybe_ShouldReturnGameByTybe()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactory(options);
                var service = new GameServices(factory);
                var game = new Game { name = "Game1", tybe = "fifa" };
                await service.Save(game);

                // Act
                var fetchedGame = await service.Get("fifa");

                // Assert
                Assert.NotNull(fetchedGame);
                Assert.Equal(game.tybe, fetchedGame.tybe);
            }

            [Fact]
            public async Task GetList_ShouldReturnGamesByName()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactory(options);
                var service = new GameServices(factory);
                await service.Save(new Game { name = "Game1", tybe = "fifa" });
                await service.Save(new Game { name = "Game2", tybe = "fifa" });

                // Act
                var games = await service.GetList("Game");

                // Assert
                Assert.Equal(2, games.Count);
            }

            [Fact]
            public async Task GetAll_ShouldReturnAllBooks()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactory(options);
                var service = new GameServices(factory);
                await service.Save(new Game { name = "Game1", tybe = "fifa" });
                await service.Save(new Game { name = "Game2", tybe = "fifa" });

                // Act
                var games = await service.GetAll();

                // Assert
                Assert.Equal(2, games.Count);
            }

            [Fact]
            public async Task Delete_ShouldRemoveGame()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactory(options);
                var service = new GameServices(factory);
                var game = new Game { name = "Game1", tybe = "fifa" };
                await service.Save(game);

                // Act
                await service.Delete(game);

                // Assert
                using var context = new Game_Hall_Context(options);
                var deletedGame = await context.games.FindAsync(game.id);
                Assert.Null(deletedGame);
            }

            [Fact]
            public async Task Update_ShouldModifyGame()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactory(options);
                var service = new GameServices(factory);
                var game = new Game { name = "Game1", tybe = "fifa" };
            await service.Save(game);

                // Act
                game.name = "Updated Game";
                game.tybe = "cod";
                await service.Update(game);

                // Assert
                using var context = new Game_Hall_Context(options);
                var updatedGame = await context.games.FindAsync(game.id);
                Assert.Equal("Updated Game", updatedGame.name);
                Assert.Equal("cod", updatedGame.tybe);
            }

            [Fact]
            public async Task AddPlayerToGame_ShouldAddPlayer()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactory(options);
                var service = new GameServices(factory);
                var game = new Game { name = "Game1", tybe = "fifa" };
                var player = new Player { Name = "Player1", phone = 1};
                await service.Save(game);

                // Act
                await service.AddPlayerToGame(game, player);

                // Assert
                using var context = new Game_Hall_Context(options);
                var savedGame = await context.games.Include(b => b.Players).FirstOrDefaultAsync(b => b.id == game.id);
                Assert.NotNull(savedGame);
                Assert.Contains(savedGame.Players, a => a.Name == "Player1");
            }

            [Fact]
            public async Task RemoveAuthorFromBook_ShouldRemoveAuthor()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactory(options);
                var service = new GameServices(factory);
                var game = new Game { name = "Game1", tybe = "fifa" };
            var player = new Player { Name = "Player1", phone = 1 };
                await service.Save(game);
                await service.AddPlayerToGame(game, player);

                // Act
                await service.RemovePlayerFromGame(game, player);

                // Assert
                using var context = new Game_Hall_Context(options);
                var savedGame = await context.games.Include(b => b.Players).FirstOrDefaultAsync(b => b.id == game.id);
                Assert.NotNull(savedGame);
                Assert.DoesNotContain(savedGame.Players, a => a.Name == "Player1");
            }
        }


















    }