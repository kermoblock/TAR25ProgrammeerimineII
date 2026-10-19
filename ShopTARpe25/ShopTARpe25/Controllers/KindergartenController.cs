using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARpe25.ApplicationServices.Services;
using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using ShopTARpe25.Models.Kindergarten;


namespace ShopTARpe25.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly IKindergartenServices _kindergartenService;
        private readonly ShopTARpe25Context _context;
        private readonly IFileService _fileService;

        //teha constructor et saaks kasutada teenust, mis on
        //defineeritud ISpaceshipServices liideses


        public KindergartenController
            (
                IKindergartenServices kindergartenService,
                ShopTARpe25Context context,
                IFileService fileService
            )
        {
            _kindergartenService = kindergartenService;
            _context = context;
            _fileService = fileService;
        }


        public IActionResult Index()
        {
            //loome baheinstantsi domaini ja viewmodeli vahel
            var result = _context.Kindergartens
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    TeacherName = x.TeacherName
                });


            return View(result);
        }

        //kui kasutaja klikib "Create" nuppu, siis see meetod käivitatakse
        //tagastab kasutajale vormi, kuhu saab sisestada andmed
        [HttpGet]
        public IActionResult Create()
        {
            KindergartenCreateViewModel vm = new();
            return View(vm);
        }

        //kui oled teinud vormi, siis see meetod käivitatakse
        //saadab andmed serverisse, kus need salvestatakse andmebaasi
        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateViewModel vm)
        {
            //luua vaheinstants, mis sisaldab andmeid, mis on saadud vormist
            //need andmed tuleb edasi saata dto-sse, mis on mõeldud andmebaasi salvestamiseks

            var dto = new KindergartenDto
            {
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                Files = vm.Files,
                FileToApiDtos = vm.Image
                    .Select(file => new FileToApiDto
                    {
                        Id = file.ImageId,
                        ExistingFilePath = file.FilePath,
                        KindergartenId = file.KindergartenId
                    }).ToArray()
            };

            //kutsuda teenuse meetodit, mis salvestab andmed andmebaasi
            var result = await _kindergartenService.Create(dto);

            return RedirectToAction(nameof(Index));
        }

        //tuleb teha details meetod
        //kutsub välja interfaceist service meetodi

        [HttpGet]

        public async Task<IActionResult> Details(Guid Id)
        {
            //meetodi kutsumine interfaceist
            var kindergarten = await _kindergartenService.DetailsAsync(Id);

            //veakäsitlus
            //suunab vaatele NotFound, kui andmeid ei ole
            if (kindergarten == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == Id)
                .Select(y => new ImageViewModel
                {
                    FilePath = /*"/multipleFileUpload/" + */y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new KindergartenDetailsViewModel();

            vm.Id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Images.AddRange(images);

            return View(vm);

            //tuleb teha viewmodel ja see siin välja kutsuda
            //ära map-ida vm ja domain 
        }

        [HttpGet]

        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarten = await _kindergartenService.DetailsAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == id)
                .Select(y => new ImageViewModel
                {
                    FilePath =/* "/multipleFileUpload/" + */y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new KindergartenUpdateViewModel();

            vm.Id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Images.AddRange(images);


            return View(vm);
        }

        [HttpPost]

        public async Task<IActionResult> Update(KindergartenUpdateViewModel vm)
        {
            var dto = new KindergartenDto

            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                FileToApiDtos = vm.Images
                    .Select(file => new FileToApiDto
                    {
                        Id = file.ImageId,
                        ExistingFilePath = file.FilePath,
                        KindergartenId = file.KindergartenId
                    }).ToArray()
            };

            var result = await _kindergartenService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<IActionResult> Delete(Guid Id)
        {
            var kindergarten = await _kindergartenService.DetailsAsync(Id);
            if (kindergarten == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == Id)
                .Select(y => new ImageViewModel
                {
                    FilePath =/* "/multipleFileUpload/" + */y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new KindergartenDeleteViewModel();

            vm.Id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Images.AddRange(images);


            return View(vm);

        }

        [HttpPost]

        public async Task<IActionResult> DeleteConfirmation(Guid Id)
        {
            var result = await _kindergartenService.Delete(Id);

                if (result == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel vm)
        {
            //tuleb ühendada dto ja viewmodel
            var dto = new FileToApiDto()
            {
                Id = vm.ImageId
            };

            //kutsuda tenuse meetodit, mis kustutab pildi andmebaasist
            var image = await _fileService.RemoveImageFromApi(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

