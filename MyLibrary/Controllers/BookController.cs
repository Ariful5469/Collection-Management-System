using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Controllers
{
    [Authorize] // Ensures that the user must be authenticated
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private const int PageSize = 10; // Define PageSize for pagination

        public BookController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // For Admins and Users: Add or Edit Books
        [HttpGet]
        [Authorize(Roles = "Admin, User")] // Allow both Admin and User roles
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")] // Allow both Admin and User roles
        public IActionResult AddProduct(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (model.Photo != null)
                {
                    string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
                    filename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, filename);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(stream);
                    }
                }

                Book book = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    PublishedDate = model.PublishedDate,
                    ISBN = model.ISBN,
                    Description = model.Description,
                    ImagePath = filename
                };

                _context.Books.Add(book);
                _context.SaveChanges();
                ViewBag.success = "Record added successfully!";
                return View();
            }
            return View(model);
        }

        // For Admins Only: Edit Book
        [HttpGet]
        [Authorize(Roles = "Admin")] // Restrict access to users with the "Admin" role
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            var model = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                ISBN = book.ISBN,
                Description = book.Description,
                // Not including the image path since the user may want to update the image
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Restrict access to users with the "Admin" role
        public IActionResult Edit(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.Find(model.Id);
                if (book == null)
                {
                    return NotFound();
                }

                if (model.Photo != null)
                {
                    string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
                    string filename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, filename);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(stream);
                    }
                    book.ImagePath = filename;
                }

                book.Title = model.Title;
                book.Author = model.Author;
                book.PublishedDate = model.PublishedDate;
                book.ISBN = model.ISBN;
                book.Description = model.Description;

                _context.SaveChanges();
                ViewBag.success = "Book updated successfully!";
                return View(model);
            }
            return View(model);
        }

        // For All Users: View Book List
        [AllowAnonymous] // Allow all users to view the book list
        public IActionResult BookList1(string searchQuery, string selectedTag, int page = 1)
        {
            const int pageSize = 10;
            var books = _context.Books.AsQueryable();

            // Apply tag filter
            if (!string.IsNullOrEmpty(selectedTag))
            {
                books = books.Where(b => b.Tags.Contains(selectedTag));
            }

            // Apply search query filter
            if (!string.IsNullOrEmpty(searchQuery))
            {
                books = books.Where(b => b.Description.Contains(searchQuery));
            }

            var totalBooks = books.Count();
            var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);
            var currentPage = page;

            var pagedBooks = books
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewData["SearchQuery"] = searchQuery;
            ViewData["SelectedTag"] = selectedTag;
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = currentPage;

            return View(pagedBooks);
        }




        // For Admins Only: View All Books
        [HttpGet]
        [Authorize(Roles = "Admin")] // Restrict access to users with the "Admin" role
        public IActionResult BookList()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        // For Admins Only: Delete a Book
        [HttpPost]
        [Authorize(Roles = "Admin")] // Restrict access to users with the "Admin" role
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            // Optionally delete the image file
            if (!string.IsNullOrEmpty(book.ImagePath))
            {
                string filePath = Path.Combine(_env.WebRootPath, "uploads", book.ImagePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("BookList");
        }

    }

}
