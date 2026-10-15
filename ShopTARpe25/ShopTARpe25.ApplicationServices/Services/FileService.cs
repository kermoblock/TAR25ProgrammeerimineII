using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopTARpe25.ApplicationServices.Services
{
    public class FileService : IFileService
    {
        private readonly ShopTARpe25Context _context;
        private readonly IHostEnvironment _webHost;

        public FileService
            (
                ShopTARpe25Context context,
                IHostEnvironment webHost
            )
        {
            _context = context;
            _webHost = webHost;
        }

        public void FilesToAPI(KindergartenDto dto, Kindergarten domain)
        {
            //kontrollib, kas on olemas üldsegi faile
            if (dto.Files != null & dto.Files.Count > 0)
            {
                //kontrollib, kas wwwroot-is on multipleFileUpload directory-t
                if (!Directory.Exists(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"))
                {
                    //kui näeb et ei ole, siis teeb selle directory wwwrooti alla
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    //meil on vaja teha muutuja nimega uploadsFolder
                    //sinna muutuja taha on vaja Path kombineerida
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");

                    //igale failile unikaalne Guid selle nime ette
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                   //kombineerib faili Id ja upload folderi, et saada pathi
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                        //domaini teha FileToApi
                        //FileToApi
                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            KindergartenId = domain.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
            }
        }
    }
}
