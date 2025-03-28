﻿using Microsoft.EntityFrameworkCore;
using Persons.API.Database.Entities;

namespace Persons.API.Database
{
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<FamilyMemberEntity> FamilyGroup { get; set; }

    }
}
