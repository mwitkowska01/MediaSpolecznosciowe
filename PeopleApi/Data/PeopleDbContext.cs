using Microsoft.EntityFrameworkCore;
using PeopleApi.Models;
using System;

namespace PeopleApi.Data
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options) { }
        public DbSet<People> People { get; set; }
    }
}
