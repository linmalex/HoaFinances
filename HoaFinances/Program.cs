using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Driver;
using System;

namespace HoaFinances
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Paste full directory path to location of PDF files");
            var path = Console.ReadLine();
            StatementParser statementParser = new StatementParser();
            statementParser.ProcessDirectory(path);
        }

    }
}
