using Microsoft.AspNetCore.Mvc;
using LaboratoriumAsp.Models;

namespace LabApp.Controllers;

public class BookController : Controller
{
    [HttpGet]
    public IActionResult Create() => View(new BookFormViewModel());

    [HttpPost]
    public IActionResult Create(BookFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        BookStoreMemory.Books.Add(new Book
        {
            Id = BookStoreMemory.Books.Max(b => b.Id) + 1,
            Title = vm.Title,
            Isbn = vm.Isbn,
            PublicationDate = vm.PublicationDate,
            NumPages = vm.NumPages,
            Price = vm.Price,
            Author = vm.Author
        });

        TempData["Success"] = $"Dodano książkę „{vm.Title}”.";
        return RedirectToAction(nameof(Index));
    }
}
