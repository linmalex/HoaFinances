using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

[assembly: FunctionsStartup(typeof(HoaTreasurerFunctions.Startup))]
namespace HoaTreasurerFunctions
{
    public class Startup : FunctionsStartup
    {
        //TODO fix dependency injection
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddSingleton<MongoService>();

            //builder.Services.AddSingleton<IMongoService>((s) => {
            //    return new MongoService();
            //});

            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}
