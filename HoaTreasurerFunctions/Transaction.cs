using System;
using System.IO;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using System.Linq;

namespace HoaTreasurerFunctions
{
    public class Transaction
    {
        public BsonObjectId Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public static List<Transaction> GetMongoTransactions()
        {
            string connectionString = Environment.GetEnvironmentVariable("MongoConnectionString", EnvironmentVariableTarget.Process);
            string databaseName = Environment.GetEnvironmentVariable("DatabaseName", EnvironmentVariableTarget.Process);
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<Transaction> mongoTransactions = database.GetCollection<Transaction>("Transactions");
            return mongoTransactions.Find(x => true).ToList();
        }
        public static void CreateTransactions(List<Transaction> transactions)
        {
            string connectionString = System.Environment.GetEnvironmentVariable("MongoConnectionString", EnvironmentVariableTarget.Process);
            string databaseName = System.Environment.GetEnvironmentVariable("DatabaseName", EnvironmentVariableTarget.Process);
            var parser = new StatementParser();
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            IMongoCollection<Transaction> mongoTransactions = database.GetCollection<Transaction>("Transactions");
            mongoTransactions.InsertMany(transactions);
        }
    }
}
