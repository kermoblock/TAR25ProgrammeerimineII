using Microsoft.EntityFrameworkCore;
using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopTARpe25.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopTARpe25Context _context;

        public RealEstateServices
            (
                ShopTARpe25Context context
            )
        {
            _context = context;
        }
        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            RealEstate domain = new();

            domain.Id = Guid.NewGuid();
            domain.Area = dto.Area;
            domain.Address = dto.Address;
            domain.RoomNumber = dto.RoomNumber;
            domain.BuildingType = dto.BuildingType;
            domain.CreatedAt = DateTime.Now;
            domain.ModifiedAt = DateTime.Now;

            await _context.RealEstates.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;

        }


        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            RealEstate domain = new();

            domain.Id = dto.Id;
            domain.Area = dto.Area;
            domain.Address = dto.Address;
            domain.RoomNumber = dto.RoomNumber;
            domain.BuildingType = dto.BuildingType;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;

            _context.RealEstates.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<RealEstate> DetailsAsync(Guid Id)
        {
            var result = await _context.RealEstates
            .FirstOrDefaultAsync(x => x.Id == Id);


            return result;
        }

        public async Task<RealEstate> Delete(Guid Id)
        {

            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == Id);

            _context.RealEstates.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
