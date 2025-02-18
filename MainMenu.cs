using System;
using BankingSystem.Models;
using BankingSystem.Services;

public class MainMenu
{
    private Bank _bank;

    public MainMenu(Bank bank)
    {
        _bank = bank;
    }

    public void Display()
    {
        while (true)
        {
            Console.WriteLine("===== Banking System =====");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Create Account");
            Console.WriteLine("3. Deposit Funds");
            Console.WriteLine("4. Withdraw Funds");
            Console.WriteLine("5. Transfer Funds");
            Console.WriteLine("6. View Account Balance");
            Console.WriteLine("7. View Transaction History");
            Console.WriteLine("8. Exit and Save Data");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddUser();
                    break;
                case "2":
                    CreateAccount();
                    break;
                case "3":
                    DepositFunds();
                    break;
                case "4":
                    WithdrawFunds();
                    break;
                case "5":
                    TransferFunds();
                    break;
                case "6":
                    ViewAccountBalance();
                    break;
                case "7":
                    ViewTransactionHistory();
                    break;
                case "8":
                    _bank.SaveData();
                    Console.WriteLine("Data saved. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void AddUser()
    {
        Console.Write("Enter user name: ");
        string name = Console.ReadLine();
        Console.Write("Enter user ID: ");
        string userId = Console.ReadLine();
        Console.Write("Enter contact info: ");
        string contactInfo = Console.ReadLine();

        User user = new User(name, userId, contactInfo);
        _bank.AddUser(user);
        Console.WriteLine("User added successfully!");
    }

    private void CreateAccount()
    {
        Console.Write("Enter user ID: ");
        string userId = Console.ReadLine();

        var user = _bank.FindUser(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine("Select account type: ");
        Console.WriteLine("1. Savings Account");
        Console.WriteLine("2. Checking Account");
        string accountType = Console.ReadLine();

        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();

        if (accountType == "1")
        {
            Console.Write("Enter interest rate (e.g., 0.05 for 5%): ");
            decimal interestRate = decimal.Parse(Console.ReadLine());
            user.AddAccount(new SavingsAccount(accountNumber, interestRate));
        }
        else if (accountType == "2")
        {
            Console.Write("Enter overdraft limit: ");
            decimal overdraftLimit = decimal.Parse(Console.ReadLine());
            Console.Write("Enter transaction fee: ");
            decimal transactionFee = decimal.Parse(Console.ReadLine());
            user.AddAccount(new CheckingAccount(accountNumber, overdraftLimit, transactionFee));
        }
        else
        {
            Console.WriteLine("Invalid account type.");
        }

        Console.WriteLine("Account created successfully!");
    }

    private void DepositFunds()
    {
        Console.Write("Enter user ID: ");
        string userId = Console.ReadLine();

        var user = _bank.FindUser(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine("Select an account:");
        var accounts = user.GetAccounts();
        for (int i = 0; i < accounts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {accounts[i].GetAccountNumber()}");
        }

        int accountChoice = int.Parse(Console.ReadLine()) - 1;
        if (accountChoice < 0 || accountChoice >= accounts.Count)
        {
            Console.WriteLine("Invalid account choice.");
            return;
        }

        var account = accounts[accountChoice];

        Console.Write("Enter deposit amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        try
        {
            account.Deposit(amount);
            Console.WriteLine($"Deposited {amount:C} to account {account.GetAccountNumber()} successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void WithdrawFunds()
    {
        Console.Write("Enter user ID: ");
        string userId = Console.ReadLine();

        var user = _bank.FindUser(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine("Select an account:");
        var accounts = user.GetAccounts();
        for (int i = 0; i < accounts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {accounts[i].GetAccountNumber()}");
        }

        int accountChoice = int.Parse(Console.ReadLine()) - 1;
        if (accountChoice < 0 || accountChoice >= accounts.Count)
        {
            Console.WriteLine("Invalid account choice.");
            return;
        }

        var account = accounts[accountChoice];

        Console.Write("Enter withdrawal amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        try
        {
            account.Withdraw(amount);
            Console.WriteLine($"Withdrew {amount:C} from account {account.GetAccountNumber()} successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void TransferFunds()
    {
        Console.Write("Enter user ID for source account: ");
        string sourceUserId = Console.ReadLine();

        var sourceUser = _bank.FindUser(sourceUserId);
        if (sourceUser == null)
        {
            Console.WriteLine("Source user not found.");
            return;
        }

        Console.WriteLine("Select the source account:");
        var sourceAccounts = sourceUser.GetAccounts();
        for (int i = 0; i < sourceAccounts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {sourceAccounts[i].GetAccountNumber()}");
        }

        int sourceAccountChoice = int.Parse(Console.ReadLine()) - 1;
        if (sourceAccountChoice < 0 || sourceAccountChoice >= sourceAccounts.Count)
        {
            Console.WriteLine("Invalid account choice.");
            return;
        }

        var sourceAccount = sourceAccounts[sourceAccountChoice];

        Console.Write("Enter user ID for destination account: ");
        string destinationUserId = Console.ReadLine();

        var destinationUser = _bank.FindUser(destinationUserId);
        if (destinationUser == null)
        {
            Console.WriteLine("Destination user not found.");
            return;
        }

        Console.WriteLine("Select the destination account:");
        var destinationAccounts = destinationUser.GetAccounts();
        for (int i = 0; i < destinationAccounts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {destinationAccounts[i].GetAccountNumber()}");
        }

        int destinationAccountChoice = int.Parse(Console.ReadLine()) - 1;
        if (destinationAccountChoice < 0 || destinationAccountChoice >= destinationAccounts.Count)
        {
            Console.WriteLine("Invalid account choice.");
            return;
        }

        var destinationAccount = destinationAccounts[destinationAccountChoice];

        Console.Write("Enter transfer amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        try
        {
            _bank.TransferFunds(sourceAccount, destinationAccount, amount);
            Console.WriteLine($"Transferred {amount:C} from {sourceAccount.GetAccountNumber()} to {destinationAccount.GetAccountNumber()} successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ViewAccountBalance()
    {
        Console.Write("Enter user ID: ");
        string userId = Console.ReadLine();

        var user = _bank.FindUser(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine("Select an account:");
        var accounts = user.GetAccounts();
        for (int i = 0; i < accounts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {accounts[i].GetAccountNumber()}");
        }

        int accountChoice = int.Parse(Console.ReadLine()) - 1;
        if (accountChoice < 0 || accountChoice >= accounts.Count)
        {
            Console.WriteLine("Invalid account choice.");
            return;
        }

        var account = accounts[accountChoice];
        Console.WriteLine($"Account {account.GetAccountNumber()} has a balance of {account.GetBalance():C}.");
    }

        private void ViewTransactionHistory()
    {
        Console.Write("Enter user ID: ");
        string userId = Console.ReadLine();

        var user = _bank.FindUser(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine("Select an account:");
        var accounts = user.GetAccounts();
        for (int i = 0; i < accounts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {accounts[i].GetAccountNumber()}");
        }

        // Fix: Parse the user's choice as an integer Fixed this bug with ChatGPT when testing the program. Minimal changes, majority of the code was my own.
        if (!int.TryParse(Console.ReadLine(), out int accountChoice) || accountChoice < 1 || accountChoice > accounts.Count)
        {
            Console.WriteLine("Invalid account choice. Please choose a number.");
            return;
            // 2/28/2025: Not sure if this is continuing the program and allows the to ReadLine
            // TO DO: Refactor the code above to be completely mine and not chatGPT.
        }

        var account = accounts[accountChoice - 1];

        Console.WriteLine("Transaction History:");
        foreach (var transaction in account.GetTransactionHistory())
        {
            Console.WriteLine(transaction);
        }
    }
}
