using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Viex.WebSockets.Core.Repositories;

namespace Viex.WebSockets.Core
{
    public static class Startup
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddDbContext<ViexContext>(options => options.UseInMemoryDatabase("Viex.WebSockets.Database"));
            services.AddScoped<GameConceptRepository>();
            services.AddScoped<GameRoomRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<WaitingRoomRepository>();
        }
    }
}
