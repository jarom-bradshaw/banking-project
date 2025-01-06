// Handles reading/writing user and account data

// Saves and retrieves user, account, and transaction data (e.g., to/from JSON or CSV files).
// Manages file I/O for persistence.

// I added this afterword to consider how to add everything

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
                // I did this part with ChatGPT until the next //. I am unfamiliar with JSON still and have had a bunch of issues.
                // I am also still unfamiliar with try and catch.
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
                return users ?? new List<User>(); // If it doesn't return users, it will default to the right and return a empty list.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users: {ex.Message}");
                return new List<User>();
            }
        }
    }
}
