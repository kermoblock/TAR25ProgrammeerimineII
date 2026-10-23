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

            await _context.Loans.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
