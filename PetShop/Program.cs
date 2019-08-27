using Microsoft.Extensions.DependencyInjection;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Impl;
using PetShop.Core.DomainService;
using PetShop.Infrastructure;
using PetShop.Infrastructure.Data;
using PetShop.UI.ConsoleApp;
using System;

namespace PetShop
{
    class PetShop
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IPetApplicationService, PetApplicationService>();
            collection.AddScoped<IPetRepository, PetRepository>();
            collection.AddScoped<IPetShopConsoleApp, PetShopConsoleApp>();
            collection.AddScoped<IOwnerRepository, OwnerRepository>();
            var serviceProvider = collection.BuildServiceProvider();
            var petShopConsoleApp = serviceProvider.GetRequiredService<IPetShopConsoleApp>();

            FakeDatabase.InitDatabase();
            petShopConsoleApp.StartProgram();
        }
    }
}
