using System;
using System.Collections.Generic;

namespace BankingSystem.Models
{
    public abstract class Account
    {
        // 
        private string _accountNumber;
        private decimal _balance;
        private List<string> _transactionHistory; // Store transactions as strings

        // 
        protected decimal Fee;

        // Constructor
        public Account(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
                throw new ArgumentException("Account number cannot be null or empty.");

            _accountNumber = accountNumber;
            _balance = 0m; // Initialize balance to zero
            _transactionHistory = new List<string>(); // Initialize transaction history
        }

        // 
        public string GetAccountNumber()
        {
            return _accountNumber;
        }

        // 
        public decimal GetBalance()
        {
            return _balance;
        }

        // 
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            _balance += amount;
            AddTransaction($"Deposited {amount:C}");
        }

        // 
        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            if (amount > _balance)
                throw new InvalidOperationException("Insufficient funds for this transaction.");

            _balance -= amount;
            AddTransaction($"Withdrew {amount:C}");
        }

        // 
        public abstract decimal CalculateInterest();

        // Transaction management. I had to add this for case7 of my main menu to work from my UserChoiceModel.png
        protected void AddTransaction(string transaction)
        {
            _transactionHistory.Add($"{DateTime.Now}: {transaction}");
        }

        public IEnumerable<string> GetTransactionHistory()
        {
            return _transactionHistory.AsReadOnly(); // Prevent external modification
        }

        // Public wrapper for adding transactions so that Bank.cs doesn't have to use AddTransaction and we still use encapsulation.
        public void LogTransaction(string description, decimal amount)
        {
            AddTransaction($"{description}: {amount:C}");
        }
    }
}
