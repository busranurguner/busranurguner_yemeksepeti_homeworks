using BookStore.Data.BooksAggregate;
using BookStore.Data.CatalogueAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IBooksRepository Books { get; }
        ICatalogueRepository Catalogues { get; }
        int Complete();
    }
}
