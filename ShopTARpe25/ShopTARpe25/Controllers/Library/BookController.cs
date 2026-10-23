using Microsoft.AspNetCore.Mvc;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using ShopTARpe25.LINQ;
using ShopTARpe25.Models.Library;

namespace ShopTARpe25.Controllers.Library
{
    public class BookController : Controller
    {
        private readonly IBookServices _bookServices;
        private readonly LibraryContext _context;
        private readonly LINQSorting _linqSorting;

        public BookController
            (
            IBookServices bookServices,
            LibraryContext context,
            LINQSorting linqSorting
            )
        {
            _bookServices = bookServices;
            _context = context;
            _linqSorting = linqSorting;
        }

        [HttpGet]
        public IActionResult Create()
        {
            BookCreateViewModel vm = new();

            return View(vm);
            
        }
        public IActionResult Index()
        {
            var result = _context.Books
            .Select(x => new BookIndexViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                PublishedYear = x.PublishedYear,
                IsAvailable = x.IsAvaliable
            });



            return View(result);
        }

        // kui oled teinud vormi, siis see meetod käivitatakse
        // saadab andmed serverisse, kus need salvestatakse andmebaasi
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new BookDto
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    Author = vm.Author,
                    PublishedYear = vm.PublishedYear,
                    IsAvailable = vm.IsAvailable
                };



                var result = await _bookServices.Create(dto);

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
    }
}
