﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View("CustomerForm", customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes
                .ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = new Customer(),
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                _context.Customers.Add(customer);
            }
               
            else
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                var customerInDb = _context.Customers
                    .Single(c => c.Id == customer.Id);
                // Mapper.Map(customer, customerInDb);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IssubscribedToNewsletter = customer.IssubscribedToNewsletter;

                // not recommanded: security problem
                TryUpdateModel(customerInDb);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        //[HttpPost]
        //public ActionResult Create(Customer customer)
        //{
        //    _context.Customers.Add(customer);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index", "Customers");
        //}

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers
                .SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "John Smith" },
                new Customer { Id = 2, Name = "Mary Williams" }
            };
        }
    }
}