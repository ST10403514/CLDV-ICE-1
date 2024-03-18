using Microsoft.AspNetCore.Mvc;
using MyBudgetApp.Models;
using System;
using System.Collections.Generic;

namespace MyBudgetApp.Controllers
{
    public class TransactionController : Controller
    {
        private static List<Transaction> transactions = new List<Transaction>();

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost] // /Transaction/Add
        public IActionResult Add(Transaction transaction)
        {
            transaction.Id = transactions.Count + 1;
            transaction.Date = DateTime.Now;
            transactions.Add(transaction);

            return RedirectToAction("Log");
        }

        // /Transaction/Log
        public IActionResult Log()
        {
            // Calculate total
            decimal total = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.Type == "Income")
                {
                    total += transaction.Amount;
                }
                else if (transaction.Type == "Expense")
                {
                    total -= transaction.Amount;
                }
            }

            ViewData["Total"] = total;
            return View(transactions);
        }

    }
}
