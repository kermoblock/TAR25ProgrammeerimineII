using System;
using System.Collections.Generic;
using System.Text;

namespace ShopTARpe25.Core.Domain
{
    public class FileToDatabase
    {
        public Guid Id { get; set; }
        public string? ImageTitle { get; set; }

        //kuna pilt tehtakse väikesteks byte-ideks
        public byte[]? ImageData { get; set; }
        public Guid? RealEstateId { get; set; }
    }
}
