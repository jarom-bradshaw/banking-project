// Handles reading/writing user and account data

// Saves and retrieves user, account, and transaction data (e.g., to/from JSON or CSV files).
// Manages file I/O for persistence.


using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using BankingSystem.Models;

namespace BankingSystem.Data
{
    public static class DataStore
    {
        //
        private const string UsersFilePath = "users.json";

        //
        public static void SaveUsers(List<User> users)
        {
            try
            {
                // I am still unfamiliar with making robust code by using try and catch. I referenced chatGPT and
                // W3Schools (https://www.w3schools.com/cs/cs_exceptions.php) for syntax until the next "//"
                string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(UsersFilePath, json);
                Console.WriteLine("Users saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving users: {ex.Message}");
            }
            // 
        }

        //
        public static List<User> LoadUsers()
        {
            try
            {
                if (!File.Exists(UsersFilePath))
                {
                    Console.WriteLine("No existing user data found. Starting with an empty list.");
                    return new List<User>();
                }

                
                string json = File.ReadAllText(UsersFilePath);
                var users = JsonSerializer.Deserialize<List<User>>(json);

                Console.WriteLine("Users loaded successfully.");
                return users ?? new List<User>(); // If it doesn't return users, it will default to the right and return
                                                  // a empty list.
                                                  // 2/18/2025: I forgot what this does. Here is a reference. ?? returns
                                                  // the value of the left if it isn't null, otherwise it will return
                                                  // the right value.
                                                  // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/
                                                  // operators/null-coalescing-operator
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users: {ex.Message}");
                return new List<User>();
            }
        }
    }
}
