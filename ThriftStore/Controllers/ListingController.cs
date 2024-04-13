using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThriftStore.Models;
using ThriftStore.Data;
using System.Linq;

public class ListingController : Controller
{
    private readonly ApplicationDbContext _context;

    public ListingController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Displays the list of all listings.
    public IActionResult ListingIndex()
    {
        var allListings = _context.Listings.ToList();
        return View(allListings);
    }

    // Displays details of a specific listing.
    public IActionResult ListingDetails(int id)
    {
        var listing = _context.Listings.FirstOrDefault(l => l.ListingID == id);

        if (listing == null)
        {
            return NotFound();
        }

        return View(listing);
    }

    // Displays the form to create a new listing.
    [HttpGet]
    public IActionResult ListingCreate()
    {
        var newListing = new Listing();
        return View(newListing);
    }

    // Handles the creation of a new listing.
    [HttpPost]
    public IActionResult ListingCreate(Listing listing)
    {
        if (ModelState.IsValid)
        {
            _context.Listings.Add(listing);
            _context.SaveChanges();

            return RedirectToAction("ListingIndex");
        }

        return View(listing);
    }

    // Displays the confirmation page for deleting a listing.
    [HttpGet]
    public IActionResult ListingDelete(int id)
    {
        var listing = _context.Listings.FirstOrDefault(l => l.ListingID == id);

        if (listing == null)
        {
            return NotFound();
        }

        return View(listing);
    }

    // Handles the deletion of a listing.
    [HttpPost, ActionName("ListingDelete")]
    public IActionResult ListingDeleteConfirmed(int id)
    {
        var listing = _context.Listings.FirstOrDefault(l => l.ListingID == id);

        if (listing == null)
        {
            return NotFound();
        }

        _context.Listings.Remove(listing);
        _context.SaveChanges();

        return RedirectToAction("ListingIndex");
    }
}
