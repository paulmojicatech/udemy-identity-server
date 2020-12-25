using System;
using Microsoft.EntityFrameworkCore;

namespace BankOfDotNetApi.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }

        public DbSet<Customers> Customers { get; set; }
    }
}
