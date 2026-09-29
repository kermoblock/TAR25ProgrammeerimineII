using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;


namespace ShopTARpe25.ApplicationServices.Services
{


    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARpe25Context _context;

        public KindergartenServices
            (
                ShopTARpe25Context context
            )
        {
            _context = context;
        }
        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten domain = new();

            domain.Id = dto.Id;
            domain.GroupName = dto.GroupName;
            domain.ChildrenCount = dto.ChildrenCount;
            domain.KindergartenName = dto.KindergartenName;
            domain.TeacherName = dto.TeacherName;
            domain.CreatedAt = DateTime.Now;
            domain.UpdatedAt = DateTime.Now;

            //siia tuleb kood, mis salvestab domain
            //objekti andmebaasi
            //tuleb kasutada repository'd, mis
            //on defineeritud Core projektis
            //konstruktori kaudu tuleb injectida repository

            await _context.Kindergartens.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;

        }

        public async Task<Kindergarten> DetailsAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);


            return result;
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            Kindergarten spaceship = new();

            spaceship.Id = dto.Id;
            spaceship.GroupName = dto.GroupName;
            spaceship.ChildrenCount = dto.ChildrenCount;
            spaceship.KindergartenName = dto.KindergartenName;
            spaceship.TeacherName = dto.TeacherName;
            spaceship.CreatedAt = dto.CreatedAt;
            spaceship.UpdatedAt = DateTime.Now;

            _context.Kindergartens.Update(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;
        }

        public async Task<Kindergarten> Delete(Guid Id)
        {

            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == Id);


            _context.Kindergartens.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
