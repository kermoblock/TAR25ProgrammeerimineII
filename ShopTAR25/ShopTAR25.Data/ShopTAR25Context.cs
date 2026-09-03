using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopTAR25.Data
{
    //teha sellest classist DBContext, et saaks andmebaasi kasutada
    public class ShopTAR25Context : DbContext
    {
        public ShopTAR25Context(DbContextOptions<ShopTAR25Context> options) : base(options)
        { }
        //teha Core projekt alla Domain nimega kaust ja sinna class nimega Spaceship.cs
    }
}
