﻿using LiftOff.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftOff.Data
{
    public class JobDbContext : DbContext

        // TODO: FIX ADDING ITEM: IDENTITY IS SET TO OFF

    {
        public DbSet<Job> Job { get; set; }

        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
