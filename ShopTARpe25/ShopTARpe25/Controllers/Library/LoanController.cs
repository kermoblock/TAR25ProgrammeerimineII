using Microsoft.AspNetCore.Mvc;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using ShopTARpe25.Models.Loan;

namespace ShopTARpe25.Controllers.Library
{
    public class LoanController : Controller
    {
        private readonly ILoanServices _loanServices;
        private readonly LibraryContext _context;

        public LoanController
            (
                ILoanServices loanServices,
                LibraryContext context
            )
        {
            _loanServices = loanServices;
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadDropdowns();

            LoanCreateViewModel vm = new()
            {
                LoanDate = DateTime.Now
            };

            return View(vm);
        }

        public IActionResult Index()
        {
            var result = _context.Loans
                .Select(x => new LoanIndexViewModel
                {
                    Id = x.Id,
                    BookId = x.BookId,
                    ReaderId = x.ReaderId,
                    LoanDate = x.LoanDate,
                    ReturnDate = x.ReturnDate
                })
                .ToList();

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var bookExists = _context.Books.Any(x => x.Id == vm.BookId);
                var readerExists = _context.Readers.Any(x => x.Id == vm.ReaderId);

                if (!bookExists || !readerExists)
                {
                    ModelState.AddModelError("", "Please select a valid book and reader.");
                }
                else
                {
                    var dto = new LoanDto
                    {
                        Id = vm.Id,
                        BookId = vm.BookId,
                        ReaderId = vm.ReaderId,
                        LoanDate = DateTime.Now,
                        ReturnDate = null
                    };

                    await _loanServices.Create(dto);

                    return RedirectToAction(nameof(Index));
                }
            }

            LoadDropdowns();
            return View(vm);
        }

        private void LoadDropdowns()
        {
            ViewBag.Books = _context.Books
                .Where(x => x.IsAvaliable)
                .OrderBy(x => x.Title)
                .ToList();

            ViewBag.Readers = _context.Readers
                .OrderBy(x => x.FullName)
                .ToList();
        }
    }
}
