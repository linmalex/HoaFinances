using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Driver;

namespace HoaFinances
{
    class StatementParser
    {
        //TODO add dependency injection for PdfTool
        public void ProcessDirectory(string dirPath)
        {
            PdfTool pdfTool = new PdfTool();
            List<FileInfo> textFiles = new DirectoryInfo(dirPath).GetFiles().ToList();
            foreach (FileInfo textFile in textFiles)
            {
                string statementText = pdfTool.ExtractPdfText(textFile);

                List<Transaction> electronicCheckingTransactions = GetCheckingTransactions(statementText).Select(t => ParseETransactionsString(t)).ToList();
                List<Transaction> paperCheckingTransactions = GetChecks(statementText).Select(t => ParseCheckingString_PAper(t)).ToList();
                List<Transaction> savingsTransactions = GetSavingsTransactions(statementText).Select(t => ParseETransactionsString(t)).ToList();

            }
        }
        public Transaction ParseCheckingString_PAper(string transactionString)
        {
            //TODO fix date processing
            double x = double.Parse(transactionString);
            Transaction t = new Transaction() { Amount = x };
            return t;
        }

        public Transaction ParseETransactionsString(string transactionString)
        {
            Regex debitPattern = new Regex(@"\((?'amt'.+)\)");

            List<string> arr = transactionString.Split(" ").ToList();
            var description = string.Join(" ", arr.Skip(2));

            string[] dateArray = arr[0].Split(@"/");
            try
            {
                DateTime date = new DateTime(2020, int.Parse(dateArray[0]), int.Parse(dateArray[1]));
            }
            catch (Exception e)
            {

                throw;
            }

            string rawAmount = arr[1];
            var debitMatch = debitPattern.Match(rawAmount);

            double amount;
            if (debitMatch.Success)
            {
                string rawString = debitMatch.Groups["amt"].ToString();
                double.TryParse(rawString, out double res);
                amount = res * -1;
            }
            else
            {
                double.TryParse(arr[1].ToString(), out double res);
                amount = res;
            }

            Transaction transaction = new Transaction()
            {
                //Date = date,
                Description = description,
                Amount = amount
            };

            return transaction;
        }
        public List<string> GetChecks(string statementText)
        {
            Regex checksSection = new Regex(@"Checks Paid\nCheck # Date Amount Check # Date Amount Check # Date Amount\n(?'checks'.*)\(\* next to number indicates skipped numbers\)", RegexOptions.Singleline);
            Regex checkAmountsRegex = new Regex(@"\d+,*\d+\.\d+");
            string checksString = checksSection.Match(statementText).Groups["checks"].Value;
            return checkAmountsRegex.Matches(checksString).Select(m => m.Value).ToList();
        }

        public List<string> GetSavingsTransactions(string statementText)
        {
            Regex savingsSection = new Regex(@"Deposits\nDate Amount Transaction Description\n(?'savings'.*)Business Basic Checking - 3592864439", RegexOptions.Singleline);
            Regex savingsSubTransactions = new Regex(@"\d+\/(.*)", RegexOptions.Multiline);
            string savingsTransactionsString = savingsSection.Match(statementText).Groups["savings"].Value;
            return savingsSubTransactions.Matches(savingsTransactionsString).Select(m => m.Value).ToList();
        }

        public List<string> GetCheckingTransactions(string statementText)
        {
            Regex checkingSection = new Regex(@"Business Basic Checking - 3592864439\nDeposits\nDate Amount Transaction Description\n(?'checking'.*)Checks Paid", RegexOptions.Singleline);
            Regex checkingSubTransactionsRegex = new Regex(@"^\d+\/.*", RegexOptions.Multiline);
            string checkingTransactionsString = checkingSection.Match(statementText).Groups["checking"].Value;
            return checkingSubTransactionsRegex.Matches(checkingTransactionsString).Select(m => m.Value).ToList();
        }
    }
}
