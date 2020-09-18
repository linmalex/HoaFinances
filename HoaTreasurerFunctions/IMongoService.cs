using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(HoaTreasurerFunctions.Startup))]
namespace HoaTreasurerFunctions
{
    public interface IMongoService { }
}
