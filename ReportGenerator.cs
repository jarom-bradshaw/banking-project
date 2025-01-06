// 8. Class: ReportGenerator
// Purpose: Generates reports for accounts and users.
// Attributes:
// Useless clas?

// Methods:
// public static string GenerateAccountStatement(Account account) string block/summary/of/account
// public static string GenerateUserReport(User user) - stringblock/ user info.

// Diff between the two is one is account for savings, one is account for checking. Diff for other on is account for user provides extra basic 
// summary just for accounts. I wonder if I need to make user public. No, how can i access the information for the userreport……. Burhrhrhr
using System;
using System.IO;
using System.Text;
using BankingSystem.Models;

namespace BankingSystem.Services
{
    public static class ReportGenerator
    {
        // Generate a detailed statement for an account
        public static string GenerateAccountStatement(Account account)
        {
            if (account == null)
                throw new ArgumentException("Account cannot be null.");

            StringBuilder report = new StringBuilder();
            report.AppendLine("===== Account Statement =====");
            report.AppendLine($"Account Number: {account.GetAccountNumber()}");
            report.AppendLine($"Balance: {account.GetBalance():C}");
            report.AppendLine("Transactions:");
            report.AppendLine("-----------------------------");

            foreach (var transaction in account.GetTransactionHistory())
            {
                report.AppendLine(transaction);
            }

            report.AppendLine("-----------------------------");
            report.AppendLine($"Ending Balance: {account.GetBalance():C}");
            return report.ToString();
        }

        // Generate a user summary report
        public static string GenerateUserReport(User user)
        {
            if (user == null)
                throw new ArgumentException("User cannot be null.");

            StringBuilder report = new StringBuilder();
            report.AppendLine("===== User Summary Report =====");
            report.AppendLine($"Name: {user.GetName()}");
            report.AppendLine($"User ID: {user.GetUserId()}");
            report.AppendLine($"Contact Info: {user.GetContactInfo()}");
            report.AppendLine("Accounts:");
            report.AppendLine("-----------------------------");

            foreach (var account in user.GetAccounts())
            {
                report.AppendLine($"Account Number: {account.GetAccountNumber()}");
                report.AppendLine($"Balance: {account.GetBalance():C}");
                report.AppendLine();
            }

            report.AppendLine("-----------------------------");
            report.AppendLine("End of Report");
            return report.ToString();
        }

        // Save report to a file
        public static void SaveReportToFile(string report, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("File name cannot be null or empty.");

            File.WriteAllText(fileName, report);
        }
    }
}
