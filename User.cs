//  Class: User
// Purpose: We need a user how can be ininstantiated, I need to be able to add an account to the user and manage information specific to them,
//  They will also have some contact info which could be their login info as well. Since we are a bank, I might need someway to manage their 
// address, but I can make that separate for now. If needed maybe make contact info be a dictionary with specific information. For now, I’ll 
// keep it as a string. Do I need to use this to manage everything and also connect to the transfer’s? 

// Attributes:
// What would we need?
// private string Name
// private string UserId
// private string/dict/list?? ContactInfo
// private List<Account> Accounts
// #Other ideas include account number(that would be separate actually

// Methods:


// public User(string name, string userId, string contactInfo)  (I was trying to kind fo use this as a Main(), but chatGPT helped me to use
//  user to just instantiate a user.) I’m wondering how I could create this information and generate some other information for a database. 
// Like make a primarykey for the user that can act as a foreign key in other places. I need to consider how I’ll store and save data, whether
//  I do it in a csv or json., frick bruh. complificiations

// public void AddAccount(Account account) 
// public List<Account> GetAccounts() 
// Public SetAccounts()? I might need a setter.
// public void UpdateContactInfo(string newContactInfo) - Updates contact details.

using System;
using System.Collections.Generic;

namespace BankingSystem.Models
{
    public class User
    {
        // Private fields
        private string _name;
        private string _userId;
        private string _contactInfo;
        private List<Account> _accounts;

        // Constructor
        public User(string name, string userId, string contactInfo)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.");
            
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID cannot be null or empty.");
            
            if (string.IsNullOrEmpty(contactInfo))
                throw new ArgumentException("Contact info cannot be null or empty.");

            _name = name;
            _userId = userId;
            _contactInfo = contactInfo;
            _accounts = new List<Account>(); // Initialize empty account list
        }

        // 
        public string GetName() => _name;

        // 
        public string GetUserId() => _userId;


        // 
        public string GetContactInfo() => _contactInfo;

        // 
        public void UpdateContactInfo(string newContactInfo)
        {
            if (string.IsNullOrEmpty(newContactInfo))
                throw new ArgumentException("Contact info cannot be null or empty.");

            _contactInfo = newContactInfo;
        }

        // 
        public void AddAccount(Account account) //Find out what I have wrong with access to access it
        {
            if (account == null)
                throw new ArgumentException("Account cannot be null.");

            _accounts.Add(account);
        }

        // Get the list of accounts. Idk why there is an error. It's private, but I should be able to use it within this class I thought.
        public List<Account> GetAccounts()
        {
            return _accounts;
        }
    }
}
