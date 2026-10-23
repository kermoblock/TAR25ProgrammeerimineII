using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using System;
using System.Collections.Generic;
using System.Text;
using static ShopTARpe25.ApplicationServices.Services.BookServices;

namespace ShopTARpe25.ApplicationServices.Services
{
    public class BookServices : IBookServices
    {
        private readonly LibraryContext _context;

        public BookServices(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Book> Create(BookDto dto)
        {
            Book domain = new();

            domain.Id = Guid.NewGuid();
            domain.Title = dto.Title;
            domain.Author = dto.Author;
            domain.PublishedYear = dto.PublishedYear;
            domain.IsAvaliable = true;

            await _context.Books.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
