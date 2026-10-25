using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Book> Update(BookDto dto)
        {
            var books = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (books == null)
            {
                return null;
            }

            books.Id = dto.Id;
            books.Title = dto.Title;

            await _context.SaveChangesAsync();

            return books;
        }
        public async Task<Book?> Delete(Guid id)
        {
            var result = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            _context.Books.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
