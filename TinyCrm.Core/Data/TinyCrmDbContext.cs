﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace TinyCrm.Core.Data
{
    public class TinyCrmDbContext : DbContext
    {
        private readonly string connectionString_;

        public TinyCrmDbContext()
        {
            connectionString_ =
                "Server =localhost; Database=Tiny-CRM-official; Integrated Security = SSPI; Persist Security Info=False;";
        }
        
        public TinyCrmDbContext(string connString)
        {
            connectionString_ = connString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Model.Customer>()
                .ToTable("Customer", "core");
             

            modelBuilder
                   .Entity<Model.Product>()
                   .ToTable("Product", "core");

            modelBuilder
                   .Entity<Model.Customer>()
                   .HasIndex(c => c.VatNumber).IsUnique();

            modelBuilder
                  .Entity<Model.Customer>()
                  .Property(c => c.VatNumber)
                  .HasMaxLength(9)
                  .IsFixedLength();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
