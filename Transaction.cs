// 5. Class: Transaction
// Purpose: Logs any type of transfer. 
// Attributes:
// private DateTime Timestamp
// Maybe include type of transaction:)( public list<TransactionType> ChooseTransaction()
// private string Description
// private decimal Amount
// Methods:
// public Transaction(string description/TransactionType[]?, decimal amount) - Constructor.
// ##IDK how I should do this part. I think a string description is simplest as it allows user to specify with simple string the transaction 
// instead of using case statements to make a menu for choosing the transaction type. I think the code is cleaner with less menu’s unless I 
// could make it a drop down menu, but idk how to do that in a GUI. I need to learn gYIs
// public string GetDetails() - Returns a summary do transa;ao.
using System;

namespace BankingSystem.Models
{
    public class Transaction
    {
        //
        private DateTime _timestamp;
        private string _description;
        private decimal _amount;

        //
        public Transaction(string description, decimal amount)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description cannot be null or empty.");
            
            if (amount == 0)
                throw new ArgumentException("Transaction amount cannot be zero.");

            _timestamp = DateTime.Now; 
            _description = description;
            _amount = amount;
        }

        // 
        public DateTime GetTimestamp()
        {
            return AdjustTimestamp(_timestamp);
        }

        // 
        public string GetDescription()
        {
            return FormatDescription(_description);
        }

        // 
        public decimal GetAmount()
        {
            return CalculateRoundedAmount(_amount);
        }

        //
        public string GetDetails()
        {
            return $"{GetTimestamp():G} | {GetDescription()} | {GetAmount():C}";
        }

        // helper function
        private DateTime AdjustTimestamp(DateTime timestamp)
        {
            return timestamp.AddSeconds(0); // No real adjustment,  placeholder to process GetTimestamp.
        }

        // helper function
        private string FormatDescription(string description)
        {
            return description.Trim().ToUpper(); 
        }

        // helper function
        private decimal CalculateRoundedAmount(decimal amount)
        {
            return Math.Round(amount, 2); 
        }
    }
}
