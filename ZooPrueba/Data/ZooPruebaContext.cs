using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modelos;

    public class ZooPruebaContext : DbContext
    {
        public ZooPruebaContext (DbContextOptions<ZooPruebaContext> options)
            : base(options)
        {
        }

        public DbSet<Modelos.Raza> Razas { get; set; } = default!;

public DbSet<Modelos.Especie> Especies { get; set; } = default!;

public DbSet<Modelos.Animal> Animals { get; set; } = default!;
    }
