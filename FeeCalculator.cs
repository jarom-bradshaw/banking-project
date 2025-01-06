// 7. Class: FeeCalculator

// Attributes:
// private Dictionary<string, decimal> FeeRates other than the overdraft fee’s I can charge like a 0.01 fee for every transaction so that bank 
// can make money.

// Methods:
// public FeeCalculator() - Constructor to initialize fee rates.
// public decimal GetFee(string transactionType) - 2 rates. One for saving’s and one for checking. I would like to limit the amount of savings 
// transactions you can make, but extra functionality for later.

using System;
using System.Collections.Generic;

namespace BankingSystem.Services
{
    public class FeeCalculator
    {
        //
        private Dictionary<string, decimal> _feeRates;

        // Construtor
        public FeeCalculator()
        {
            _feeRates = new Dictionary<string, decimal>
            {
                { "Savings", 0.01m }, // 1% fee for savings account transactions
                { "Checking", 0.02m }, // 2% fee for checking account transactions
                { "Overdraft", 15.00m } // Flat fee for overdraft transactions
            };
        }

        // 
        public decimal GetFee(string transactionType)
        {
            if (string.IsNullOrEmpty(transactionType))
                throw new ArgumentException("Transaction type cannot be null or empty.");

            if (_feeRates.TryGetValue(transactionType, out decimal fee))
            {
                return fee; //The amount of indentation here makes me vomit.
            }

            // Return a default fee if no specific type is found
            return 0m; // No fee by default
        }

        // 
        public void SetFee(string transactionType, decimal fee)
        {
            if (string.IsNullOrEmpty(transactionType))
                throw new ArgumentException("Transaction type cannot be null or empty.");

            if (fee < 0)
                throw new ArgumentException("Fee cannot be negative.");

            _feeRates[transactionType] = fee;
        }

        // Optional: Get all current fee rates (for administrative purposes)  : Additional capability that I did not think of that
        //  I incoporated through ChatHPT
        public Dictionary<string, decimal> GetAllFees()
        {
            return new Dictionary<string, decimal>(_feeRates); // Return a copy to preserve encapsulation
        }
    }
}
