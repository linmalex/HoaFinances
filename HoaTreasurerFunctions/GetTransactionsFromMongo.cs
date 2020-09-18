using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace HoaTreasurerFunctions
{
    public static class GetTransactionsFromMongo
    {
        [FunctionName("GetTransactions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("outqueue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> msg,
            ILogger log)
        {
            try
            {
                List<Transaction> transactions = Transaction.GetMongoTransactions();
                string responseMessage = $"There are {transactions.Count} transactions. That's all we know right now.";
                return new OkObjectResult(responseMessage);
            }
            catch (Exception e)
            {
                //this function only works when all IP addresses are allowed to connnect to db
                //TODO fix whitelisted ip addresses in mongo
                log.LogError(e.Message);
                return new StatusCodeResult(500);
            }
        }
        //TODO create mongo service class


    }
}
