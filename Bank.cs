// 6. Class: Bank
// Purpose: Looks at user’s and set’s up a simple relationship between bank and users to manage them kind’ve Main. I’m
// not sure if I need main. 

// I want to make GUI but idk how. I’ve looked at it online for a while and , not interface that’s in Main() Building
// TO DO: Desktop GUIs with C#: 
// A Comprehensive Guide – Schools bruh

// UML summary
// Attributes:
// private List<User> Users
// Methods:
// public Bank() - Constructor.
// public void AddUser(User user) - Adds a new user.
// public User FindUser(string userId) - Finds and returns a user by their ID.
// public void TransferFunds(Account fromAccount, Account toAccount, decimal amount) - Handles fund transfers between accounts.

using System;
using System.Collections.Generic;
using System.Linq; // For simplified search functionality
using BankingSystem.Data;
using BankingSystem.Models;

namespace BankingSystem.Services
{
    public class Bank
    {
        //
        private List<User> _users;

        // Constructor
        public Bank()
        {
            _users = DataStore.LoadUsers(); 
        }

        // 
        public void SaveData()
        {
            DataStore.SaveUsers(_users);
        }

        // 
        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentException("User cannot be null.");
            // ChatGPT helped with this specific line of code below to prevent duplicates. I get the concept though and
            // understand it.It would just be difficult to implement on my own.
            // UPDATE 2/18/2025
            // I was unfamiliar with lambda syntax. Code has been refactored. It is checking any object in _users
            //(List) against the specific User ID to see if they are a duplicate, and it will return true if there is a
            //userID that is the being added that is already in the list of userIDs
            if (_users.Any(u => u.GetUserId() == user.GetUserId()))
                throw new InvalidOperationException("A user with this ID already exists.");

            _users.Add(user);
        }

        //
        public List<User> GetUsers() => _users;

        // 
        public User FindUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID cannot be null or empty.");

            var user = _users.FirstOrDefault(u => u.GetUserId() == userId);

            if (user == null)
                throw new InvalidOperationException("User not found.");

            return user;
        }

        // 
        public void TransferFunds(Account fromAccount, Account toAccount, decimal amount)
        {
            if (fromAccount == null || toAccount == null)
                throw new ArgumentException("Accounts cannot be null.");

            if (amount <= 0)
                throw new ArgumentException("Transfer amount must be greater than zero.");

            if (fromAccount.GetBalance() < amount)
                throw new InvalidOperationException("Insufficient funds in the source account.");

            // Perform the transfer
            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);

            // Log the transactions 
            if (fromAccount is SavingsAccount)
                fromAccount.LogTransaction($"Transfer to {toAccount}", -amount);

            if (toAccount is SavingsAccount)
                toAccount.LogTransaction($"Transfer from {fromAccount}", amount);
        }

        // Retrieve all users (optional, for administrative purposes)
        public List<User> GetAllUsers() => _users;

        public void LoadSeedData()
        {
            var seedUsers = SeedData.Initialize();
            foreach (var user in seedUsers)
            {
                AddUser(user);
            }
        }
    }
}
