using System;
using MongoDB.Bson;

namespace HoaFinances
{
    class Transaction
    {
        public BsonObjectId id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
