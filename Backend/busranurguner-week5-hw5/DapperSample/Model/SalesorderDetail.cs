using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.Model
{
    public class SalesorderDetail
    {
        public int SalesOrderId { get; set; }

        public int ProductId { get; set; }

        public int OrderQty { get; set; }
    }
}
