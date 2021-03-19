using BookStore.Data;
using BookStore.Data.BooksAggregate;
using BookStore.Data.CatalogueAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore_Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        public IBooksRepository Books { get; }

        public ICatalogueRepository Catalogues { get; }

        public UnitOfWork(Context bookStoreDbContext,
            IBooksRepository booksRepository,
            ICatalogueRepository catalogueRepository)
        {
            this._context = bookStoreDbContext;

            this.Books = booksRepository;
            this.Catalogues = catalogueRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
