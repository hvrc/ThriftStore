using Microsoft.AspNetCore.Mvc;
using ThriftStore.Models;
using ThriftStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ThriftStore.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the list of all users.
        public IActionResult UserIndex()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // Displays details of a specific user.
        public IActionResult UserDetails(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Gets details of a user for deletion confirmation.
        [HttpGet]
        public IActionResult UserDelete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Deletes a user after confirmation.
        [HttpPost, ActionName("UserDeleteConfirmed")]
        public IActionResult UserDeleteConfirmed(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();

                return RedirectToAction("UserIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the user: " + ex.Message);
                return View("UserDelete", user);
            }
        }

        // Displays the registration form for a new user.
        [HttpGet]
        public IActionResult UserRegistration()
        {
            var newUser = new User();
            return View(newUser);
        }

        // Handles the registration of a new user.
        [HttpPost]
        public IActionResult UserRegistration(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("UserDetails", new { id = user.UserID });
            }

            return View(user);
        }

        // Displays the login form for a user.
        [HttpGet]
        public IActionResult UserLogin()
        {
            var newUser = new User();
            return View(newUser);
        }

        // Handles the login process for a user.
        [HttpPost]
        public IActionResult UserLogin(User user)
        {
            if (ModelState.IsValid)
            {
                var authenticatedUser = _context.Users.FirstOrDefault(u =>
                    u.Email == user.Email && u.Password == user.Password);

                if (authenticatedUser != null)
                {
                    return RedirectToAction("UserDetails", new { id = authenticatedUser.UserID });
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password");
            }

            return View(user);
        }

        // Displays all listings made by a specific user.
        public IActionResult UserListings(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            var userlistings = _context.Listings.Where(l => l.TempUserID == id).ToList();

            return View(userlistings);
        }

    }
}
