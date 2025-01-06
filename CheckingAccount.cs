

// Attributes
// OverdraftLimit
// Defines how much account can go into negative, will be private to enforce control.
// {

// }

// constructors base class constr set overdraft limit optionally initialize a fee. like if the user does this(randomChoice) they get a fee, if not
//  they dont
//  accepts accountNumber, overdraftLimit, and an optional transactionFee
// calls base(accountNumber) to initialize inherited attributes.

// override Withdraw
//          Allow withdrawals that exceed balance within the overdraft limit
//          apply fees for overdrafts, if meets parameters(or if required) could differ on account type, like student account or new account 
// could get the first overdraft without a fee.

// for future: customize CalculateInterest
//      checking accounts probably won't accrue interest, but I can implement a basic logic or leave it at 0. Probably returns 0, so I can 
// just do => 0

using System;

namespace BankingSystem.Models
{
    public class CheckingAccount : Account
    {
        // Attribute
        private decimal OverdraftLimit;

        // Constructor
        public CheckingAccount(string accountNumber, decimal overdraftLimit, decimal transactionFee) : base(accountNumber)
        {
            if (overdraftLimit < 0)
                throw new ArgumentException("Overdraft limit must be non-negative.");

            OverdraftLimit = overdraftLimit;
            Fee = transactionFee; // Set default transaction fee
        }

        // Override Withdraw to allow overdrafts
        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            // Check if withdrawal exceeds balance and overdraft limit
            if (amount > GetBalance() + OverdraftLimit)
                throw new InvalidOperationException("Withdrawal exceeds balance and overdraft limit.");

            // Apply overdraft fee if balance goes into negative
            if (amount > GetBalance())
            {
                decimal overdraftFee = Fee;
                Console.WriteLine($"Overdraft fee of {overdraftFee:C} applied.");
                base.Withdraw(Fee); // Deduct fee from balance
            }

            // Deduct the amount from balance
            base.Withdraw(amount);
        }

        // Checking accounts typically don't accrue interest
        public override decimal CalculateInterest()
        {
            return 0m; // No interest for checking accounts
        }
    }
}
