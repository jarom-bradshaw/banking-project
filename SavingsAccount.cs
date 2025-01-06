// 3. Class: SavingsAccount (Derived Class)
// Purpose: Like a 3.5 high yield savings account

// Attributes:
// private decimal InterestRate (i'm thinking like 3.5

// Methods:
// public SavingsAccount(string accountNumber, decimal interestRate) - constuct.
// public decimal CalculateInterest() - Computes interest based on the current balance.
// public override void Withdraw(decimal amount) - Disallows withdrawals that would overdraft.



using System;

namespace BankingSystem.Models
{
    public class SavingsAccount : Account
    {
        // attribuotes
        private decimal InterestRate;

        // Constrcuatifactor
        public SavingsAccount(string accountNumber, decimal interestRate) : base(accountNumber)
        {
            if (interestRate < 0 || interestRate > 1)
                throw new ArgumentException("Interest rate must be between 0 and 1(0-100%)");
        
            InterestRate = interestRate;
            Fee = 0m; //no transaction fee at first. If other conditions met I can stipulate transaction fee then for savings account.
        }

        // 
        public override decimal CalculateInterest() => GetBalance() * InterestRate;

        public override void Withdraw(decimal amount)
        {
            // so people don't bankrupt us
            if (amount > 10000)
                throw new InvalidOperationException("Cannot withdraw more than $10,000 in a single transaction buddy. Nice try. But no.");
            
            base.Withdraw(amount);
        }
    }
}