using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using MongoDB.Driver;

[assembly: FunctionsStartup(typeof(HoaTreasurerFunctions.Startup))]
namespace HoaTreasurerFunctions
{
    public class MongoService
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _transactions;
        public MongoService()
        {
            string connectionString = System.Environment.GetEnvironmentVariable("MongoConnectionString", EnvironmentVariableTarget.Process);
            _client = new MongoClient(connectionString);
            IMongoDatabase transactions = _client.GetDatabase("Transactions");
        }

    }
}
