using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopTARpe25.ApplicationServices.Services
{
    public class ReaderServices : IReaderServices
    {
        private readonly LibraryContext _context;

        public ReaderServices(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Reader> Create(ReaderDto dto)
        {
            Reader domain = new();

            domain.Id = Guid.NewGuid();
            domain.FullName = dto.FullName;
            domain.Email = dto.Email;

            await _context.Readers.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
