using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Viex.WebSockets.Core.Models;

namespace Viex.WebSockets.Core
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<WaitingRoom> WaitingRooms { get; set; }
    }
}
