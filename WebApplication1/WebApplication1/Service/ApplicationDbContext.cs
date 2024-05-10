﻿using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options) 

        {
            
        }

        public DbSet<NguoiDung> NguoiDung { get; set; }
    }
}
