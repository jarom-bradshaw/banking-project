using System;
using BankingSystem.Services;

class Program
{
    static void Main(string[] args)
    {
        var bank = new Bank();

        Console.WriteLine("Do you want to load seed data? (yes/no): ");
        string input = Console.ReadLine()?.ToLower();
        if (input == "yes")
        {
            bank.LoadSeedData();
            Console.WriteLine("Seed data loaded.");
        }

        var menu = new MainMenu(bank);
        menu.Display();
    }
}

