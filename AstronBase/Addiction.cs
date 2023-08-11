using AstronBase.DAL;
using AstronBase.DAL.Interfaces;
using AstronBase.DAL.Repositories;
using AstronBase.Models;
using AstronBase.Service.Implementations;
using AstronBase.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AstronBase
{
    public static class Addiction
    {
        public static void AddAddiction(WebApplicationBuilder builder, IMapper mapper1)
        {
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();

            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();

            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

            builder.Services.AddScoped<IHomeService, HomeService>();

            builder.Services.AddScoped<IRoleService, RoleService>();

            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddSingleton(mapper1);

            builder.Services.AddScoped<IFiscalRepository, FiscalRepository>();
            builder.Services.AddScoped<IFiscalService, FiscalService>();

            builder.Services.AddScoped<IEngineerRepository, EngineerRepository>();
            builder.Services.AddScoped<IEngineerService, EngineerService>();

        }
    }
}
