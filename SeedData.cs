// Initializes default data (optional, for testing)
// Populates the application with default users, accounts, or transactions (useful for development or testing).

// Might need to find a library to do that. I did this with ChatGPT to show that my program works. 

// THE TEXT BELOW WAS GENERATED WITH CHATGPT

using System.Collections.Generic;
using BankingSystem.Models;

namespace BankingSystem.Data
{
    public static class SeedData
    {
        public static List<User> Initialize()
        {
            var users = new List<User>();

            // Create User 1
            var user1 = new User("Alice Johnson", "U001", "alice@example.com");
            var savings1 = new SavingsAccount("SAV001", 0.03m); // 3% interest
            var checking1 = new CheckingAccount("CHK001", 500m, 10m); // $500 overdraft limit, $10 fee
            savings1.Deposit(5000m);
            checking1.Deposit(1000m);
            savings1.Withdraw(200m);
            user1.AddAccount(savings1);
            user1.AddAccount(checking1);
            users.Add(user1);

            // Create User 2
            var user2 = new User("Bob Smith", "U002", "bob@example.com");
            var savings2 = new SavingsAccount("SAV002", 0.05m); // 5% interest
            savings2.Deposit(10000m);
            savings2.Withdraw(1000m);
            user2.AddAccount(savings2);
            users.Add(user2);

            return users;
        }
    }
}
