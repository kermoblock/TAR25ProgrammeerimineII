using Microsoft.AspNetCore.Mvc;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using ShopTARpe25.Models.RealEstate;
using ShopTARpe25.Models.Spaceship;

namespace ShopTARpe25.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly IRealEstateServices _realEstateService;
        private readonly ShopTARpe25Context _context;

        public RealEstateController
            (
                IRealEstateServices realEstateService,
                ShopTARpe25Context context
            )
        {
            _realEstateService = realEstateService;
            _context = context;
        }


        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Area = x.Area,
                    BuildingType = x.BuildingType,
                    RoomNumber = x.RoomNumber,
                });


            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel vm = new();
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {

            var dto = new RealEstateDto
            {
                Area = vm.Area,
                Address = vm.Address,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType
            };

            var result = await _realEstateService.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]

        public async Task<IActionResult> Details(Guid Id)
        {
            var realestate = await _realEstateService.DetailsAsync(Id);

            if (realestate == null)
            {
                return NotFound();
            }


            var vm = new RealEstateDetailsViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Address = realestate.Address;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;
            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;

            return View(vm);
        }

        [HttpGet]

        public async Task<IActionResult> Update(Guid id)
        {
            var realestate = await _realEstateService.DetailsAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Address = realestate.Address;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;
            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;

            return View("CreateUpdate", vm);
        }

        [HttpPost]

        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {

            var dto = new RealEstateDto
            {
                Id = vm.Id,
                Area = vm.Area,
                Address = vm.Address,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
            };

            var result = await _realEstateService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<IActionResult> Delete(Guid Id)
        {
            var realestate = await _realEstateService.DetailsAsync(Id);
            if (realestate == null)
            {
                return NotFound();
            }



            var vm = new RealEstateDeleteViewModel();

            vm.Id = realestate.Id;
            vm.Area = realestate.Area;
            vm.Address = realestate.Address;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;
            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;

            return View(vm);

        }

        [HttpPost]

        public async Task<IActionResult> DeleteConfirmation(Guid Id)
        {
            var result = await _realEstateService.Delete(Id);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
