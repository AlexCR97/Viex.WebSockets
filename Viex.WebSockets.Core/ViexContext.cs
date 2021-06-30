using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Viex.WebSockets.Core.Models;

namespace Viex.WebSockets.Core
{
    public class ViexContext : DbContext
    {
        public ViexContext(DbContextOptions<ViexContext> options)
            : base(options)
        {

        }

        public DbSet<GameConcept> GameConcepts { get; set; }
        public DbSet<GameRoom> GameRooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WaitingRoom> WaitingRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var gameConcepts = GameConceptCategories.All.Select((concept, index) => new GameConcept { GameConceptId = index + 1, Description = concept });
            modelBuilder.Entity<GameConcept>().HasData(gameConcepts);
            Database.EnsureCreated();
        }
    }
}
