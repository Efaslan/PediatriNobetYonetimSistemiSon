using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PediatriNobetYonetimSistemi.Models;
using System.Collections.Generic;

namespace PediatriNobetYonetimSistemi.Data
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
       
        public DbSet<Asistan> Asistan { get; set; }
        public DbSet<Hoca> Hoca { get; set; }
        public DbSet<Departman> Departman { get; set; }
        public DbSet<Nobet> Nobet { get; set; }
        public DbSet<Randevu> Randevu { get; set; }
        public DbSet<AcilDurum> AcilDurum { get; set; }
        public DbSet<Admin> Admin { get; set; }

        public DbSet<HocaMusaitlik> HocaMusaitlik { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=EFA\\EFASQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;Database=PediatriNobetDB;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)  // DbContextOptions'u base class'a gönderiyoruz
        {
        }
    }
}
