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
            services.AddDbContext<MyContext>(options => options.UseInMemoryDatabase("Viex.WebSockets.Database"));
            services.AddScoped<UserRepository>();
            services.AddScoped<WaitingRoomRepository>();
        }
    }
}
