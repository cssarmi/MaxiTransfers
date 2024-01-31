using MaxiTransfers.Api.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaxiTransfers.Api
{
    public class PersistenceContext : DbContext
    {

        private readonly IConfiguration Config;

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            Config = config;
        }
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Beneficiarios> Beneficiarios { get; set; }

       
        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

      
    }
}
