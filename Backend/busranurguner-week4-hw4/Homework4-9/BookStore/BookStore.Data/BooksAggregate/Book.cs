using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.BooksAggregate
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool Avaible { get; set; }
       
    }
}
