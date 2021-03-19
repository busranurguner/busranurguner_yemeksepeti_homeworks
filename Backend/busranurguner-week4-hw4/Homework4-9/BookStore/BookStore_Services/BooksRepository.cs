using BookStore.Data.BooksAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore_Services
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        public BooksRepository(Context context) : base(context)
        {

        }

        

    }
}
