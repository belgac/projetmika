using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magicManager.Models.Account
{
    public class Name
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Address
    {
        public string name { get; set; }
        public string extra { get; set; }
        public string street { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }

    public class Account
    {
        public int idUser { get; set; }
        public string username { get; set; }
        public string country { get; set; }
        public int isCommercial { get; set; }
        public int riskGroup { get; set; }
        public int reputation { get; set; }
        public int shipsFast { get; set; }
        public int sellCount { get; set; }
        public bool onVacation { get; set; }
        public int idDisplayLanguage { get; set; }
        public Name name { get; set; }
        public Address address { get; set; }
        public int accountBalance { get; set; }
        public int bankRecharge { get; set; }
        public int paypalRecharge { get; set; }
        public int articlesInShoppingCart { get; set; }
        public int unreadMessages { get; set; }
    }

    public class RootAccount
    {
        public Account account { get; set; }
    }
}