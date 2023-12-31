﻿using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalkDBContex:DbContext
    {
        public NZWalkDBContex(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; } 
        public DbSet<Region> Region { get; set; }
        public DbSet<Walk> Walk { get; set; }
    }
}
