using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopTARpe25.ApplicationServices.Services
{
    public class LoanServices : ILoanServices
    {
        private readonly LibraryContext _context;

        public LoanServices(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Loan> Create(LoanDto dto)
        {
            Loan domain = new();

            domain.Id = Guid.NewGuid();
            domain.BookId = dto.BookId;
            domain.ReaderId = dto.ReaderId;

            domain.LoanDate = dto.LoanDate;
            domain.ReturnDate = dto.ReturnDate;

            var book = await _context.Books.FindAsync(dto.BookId);

            if (book != null && book.IsAvaliable)
            {
                book.IsAvaliable = false;

                await _context.Loans.AddAsync(domain);
                await _context.SaveChangesAsync();
            }

            return domain;
        }
        public async Task<Loan?> BookReturn(LoanDto dto)
        {
            var loan = await _context.Loans.FindAsync(dto.Id);

            if (loan == null || loan.ReturnDate != null)
            {
                return null;
            }

            var book = await _context.Books.FindAsync(loan.BookId);

            loan.ReturnDate = DateTime.Now;

            if (book != null)
            {
                book.IsAvaliable = true;
            }

            await _context.SaveChangesAsync();

            return loan;
        }
    }
}
