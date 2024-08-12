﻿// <auto-generated />
using Game_Hall.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Game_Hall.Persistance.Migrations
{
    [DbContext(typeof(Game_Hall_Context))]
    partial class Game_Hall_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GamePlayer", b =>
                {
                    b.Property<int>("Gamesid")
                        .HasColumnType("int");

                    b.Property<int>("Playersid")
                        .HasColumnType("int");

                    b.HasKey("Gamesid", "Playersid");

                    b.HasIndex("Playersid");

                    b.ToTable("GamePlayer");
                });

            modelBuilder.Entity("Game_Hall._Domain.entities.Game", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("tybe")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("id");

                    b.ToTable("games");
                });

            modelBuilder.Entity("Game_Hall._Domain.entities.Player", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("emale")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("phone")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("GamePlayer", b =>
                {
                    b.HasOne("Game_Hall._Domain.entities.Game", null)
                        .WithMany()
                        .HasForeignKey("Gamesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Game_Hall._Domain.entities.Player", null)
                        .WithMany()
                        .HasForeignKey("Playersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
