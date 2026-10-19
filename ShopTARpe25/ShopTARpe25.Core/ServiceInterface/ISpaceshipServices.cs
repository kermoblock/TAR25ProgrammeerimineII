using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;


namespace ShopTARpe25.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        Task<Book> Create(SpaceshipDto dto);
        Task<Book> DetailsAsync(Guid id);
        Task<Book> Update(SpaceshipDto dto);
        Task<Book> Delete(Guid Id);

    }
}
