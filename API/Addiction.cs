﻿using AstronBase.DAL.Interfaces;
using AstronBase.DAL.Repositories;
using AstronBase.Service.Implementations;
using AstronBase.Service.Interfaces;
using AutoMapper;

namespace API
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

            builder.Services.AddScoped<IModelRepository, ModelRepository>();
            builder.Services.AddScoped<IModelService, ModelService>();

            builder.Services.AddScoped<IRegisterStateService, RegisterStateService>();
            builder.Services.AddScoped<IRegisterStateRepository, RegisterStateRepository>();

            builder.Services.AddScoped<IStatusBlankRepository, StatusBlankRepository>();
            builder.Services.AddScoped<IStatusBlankService, StatusBlankService>();

            builder.Services.AddScoped<IStatusFiscalService, StatusFiscalService>();
            builder.Services.AddScoped<IStatusFiscalRepository, StatusFiscalRepository>();

            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IRequestRepository, RequestRepository>();

        }
    }
}
