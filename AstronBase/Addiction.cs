using AstronBase.DAL;
using AstronBase.DAL.Interfaces;
using AstronBase.DAL.Repositories;
using AstronBase.Models;
using AstronBase.Service.Implementations;
using AstronBase.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AstronBase
{
    public static class Addiction
    {
        public static void AddAddiction(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();

            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();

        }
    }
}
