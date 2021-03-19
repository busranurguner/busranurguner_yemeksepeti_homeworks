using BookStore.Data.CatalogueAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore_Services
{
    class CatalogueRepository : GenericRepository<Catalogue>, ICatalogueRepository
    {
        public CatalogueRepository(Context context) : base(context)
        {

        }
    }
}
