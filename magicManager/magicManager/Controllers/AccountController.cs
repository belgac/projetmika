using magicManager.DAL;
using magicManager.Models;
using magicManager.Models.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace magicManager.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult AccountRequest()
        {
            IAccountRepository AccRepo = new AccountRepository();

            Account result = AccRepo.GetAccount();
            if (result != null)
            {
                return View(result);
            }
            return View();
        }
    }
}