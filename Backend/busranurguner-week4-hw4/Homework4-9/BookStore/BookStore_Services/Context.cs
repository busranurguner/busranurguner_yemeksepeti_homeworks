using BookStore.Data.BooksAggregate;
using BookStore.Data.CatalogueAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore_Services
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }

    }
}
