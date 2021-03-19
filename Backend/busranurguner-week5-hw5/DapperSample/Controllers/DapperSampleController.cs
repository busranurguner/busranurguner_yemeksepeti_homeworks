using Dapper;
using DapperSample.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DapperSampleController : ControllerBase
    {
        private readonly ILogger<DapperSampleController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;


        public DapperSampleController(ILogger<DapperSampleController> logger,IMemoryCache memoryCache, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _memoryCache = memoryCache;

        }

        public T GetCache<T>(string key, Func<T> action)
        {
            object cacheItem = _memoryCache.Get(key);
            if (cacheItem is T)
                return (T)cacheItem;

            T items = action();
            _memoryCache.Set(key, items, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(1) });
            return items;
        }

        private List<Person> LoadPerson()
        {
            var sql = "Select * From [Person].[Person];";
            using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();
                //Query() metodu veri almamızı sağlayan ve model mapping'i yaparak modellerimizi 
                //dolduran metoddur. Oluşturduğumuz metod ile Person tablosunda bulunan bütün kayıtları select ederiz.
                var productList = dbConnection.Query<Person>(sql).ToList();
                return productList;
            }
        }


        [HttpGet]
        public IActionResult DapperSelect()
        {

            string key = "productList";
            var result = GetCache<List<Person>>(key, () => LoadPerson());
            return Ok();
        }


        [HttpPost]
        public IActionResult DapperInsert()
        {

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();

                string sql = "";

                //test db ye yeni kayıt ekleniyor.
                sql = @"INSERT INTO dbo.Test (Name, Surname, Phone, Age)
                                                  Values (@Name, @Surname, @Phone, @Age);";
                var affected = db.Execute(sql, new
                {
                    Name = "İrem",
                    Surname = "Yılmaz",
                    Phone = "1123",
                    Age = "29",

                });
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult DapperUpdate()
        {

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();

                string sql= "";
                var affected = 0;
                //ıd numarası 2 ve 4 olan kullanıcıların isimlerini güncelleyen. 
                sql = "Update dbo.Test Set Name = @NewName Where Id=@NewId";
                var paramsArray = new[]
                {
                       new { NewId = 2, NewName = "Yeliz"},
                       new { NewId = 4, NewName = "Esra"}
                   };
                affected = db.Execute(sql, paramsArray);
            }
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DapperDelete()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();

                string sql = "";
                var affected = 0;

                //ıd numarası  1 olan kullanıcıyı silen. 
                sql = "Delete from dbo.Test Where Id=@NewId";
                var paramsArray = new[]
                {
                       new { NewId = 1},
                     
                   };
                affected = db.Execute(sql, paramsArray);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DapperDeleteInQuery()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();

                string sql = "";
                

                // Adı Robert olan kullanıcıyı silen. 
                sql = "Delete from [Person].[Person] Where Name=@NewName";
                var paramsArray = new[]
                {
                       new { NewName = "Robert"},

                   };
                _ = db.Query<Person>(sql, paramsArray);
            }
            return Ok();
        }


        [HttpGet]
        public IActionResult DapperQueryMultiple()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();

                string sql = "";
                //QueryMultiple sayesinde aynı komut sonuçları içinde birden fazla sorgu yürütülebilir.
                //ProductId si 300 olan [Production].[Product] ve [Sales].[SalesOrderDetail] tablosundaki verileri listeler.
                sql = @"Select * From [Production].[Product] Where ProductId = @ProductId;
                       Select * From [Sales].[SalesOrderDetail] Where ProductId = @ProductId;";
                var multipleQuery = db.QueryMultiple(sql, new { ProductId = 300 });
                var products = multipleQuery.Read<Product>();
                var salesOrderDetails = multipleQuery.Read<SalesorderDetail>().ToList();

            }
            return Ok();
        }
        [HttpGet]
        public IActionResult OneToOneMapping()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();
                string sql = "";
                //Person.PersonPhone ve Person.PhoneNumberType tablosundaki ortak alana göre veri çekme işlemini yapıyoruz.
                //List döndürecek metodumuzu oluşturduk.
                sql = @"select [Person].[PhoneNumberType].[Name],[Person].[PersonPhone].[PhoneNumber] 
                        from [Person].[PhoneNumberType] INNER JOIN [Person].[PersonPhone] 
                        ON [Person].[PhoneNumberType].[PhoneNumberTypeID] = [Person].[PersonPhone].[PhoneNumberTypeID];";

                var info = db.Query<Number, NumberType, Number>(sql, (personNumber, numberType) =>
                   {
                       personNumber.NumberType = numberType;
                       return personNumber;

                   }, splitOn: "PhoneNumber").ToList();
                return (IActionResult)info;
            }
            //return Ok();
         
        }

        [HttpGet]
        public IActionResult OneToManyMapping()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();
                string sql = "";

                //Person.PersonPhone ve Person.PhoneNumberType tablosundaki ortak alana göre veri çekme işlemini yapıyoruz.
                //List döndürecek metodumuzu oluşturduk.
                sql = @"select [Person].[PhoneNumberType].[Name],[Person].[PersonPhone].[PhoneNumber] 
                        from [Person].[PhoneNumberType] INNER JOIN [Person].[PersonPhone] 
                        ON [Person].[PhoneNumberType].[PhoneNumberTypeID] = [Person].[PersonPhone].[PhoneNumberTypeID];";
               
                var NumberDictionary = new Dictionary<int,Number>();

                var info = db.Query<Number, NumberType, Number>(sql, (personNumber, numberType) =>
                {
                    Number NumberEntry;

                    if(!NumberDictionary.TryGetValue(personNumber.NumberId,out NumberEntry))
                    {
                        NumberEntry = personNumber;
                        NumberEntry.Items = new List<NumberType>();
                        NumberDictionary.Add(NumberEntry.NumberId, NumberEntry);
                    }

                    NumberEntry.Items.Add(NumberType);
                    return NumberEntry;

                    
                }, splitOn: "NumberTypeID").Distinct().ToList();
              
            }
            return Ok();

        }
        [HttpGet]
        public IActionResult DapperTransaction()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();
                string sql = "";
                //dbo.Test ve Person.Person tablosuna yeni kayıt ekleniyor.
                sql = "INSERT INTO dbo.Test (Name, Surname, Phone, Age) Values(@Name, @Surname, @Phone, @Age);";
                using (var transaction = db.BeginTransaction())
                {
                    var affectedRows = db.Execute(sql, new
                    { Name = "Mark",
                      Surname = "Jones",
                      Phone = "5556",
                      Age = "30",

                    }, transaction: transaction); ;

                    Person person = new Person()
                    {
                        PersonId = 1,
                        Name = "Julia",
                        Surname = "Robert",
                    };
                    sql = @"Insert into [Person].[Person] (Id, Name,Surname)
                                Values (@Id, @Name, @Surname)";
                    var affected = db.Execute(sql, person);

                    transaction.Commit();

                 
                }           
                
            }
            return Ok();

        }
        [HttpGet]
        public IActionResult DapperStoreProsedure()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (db.State != ConnectionState.Open)
                    db.Open();

                string sql = "";
                //Stored Procedure sorgularını kullanabilmemiz için bu metoda commandType parametresini eklememiz gerekmektedir.
                //dob.Test tablosuna yeni kayıt ekliyoruz.
                sql = "INSERT INTO dbo.Test (Name, Surname, Phone, Age) Values(@Name, @Surname, @Phone, @Age);";
                var affectedRows = db.Execute(sql, new
                {
                    Name = "Mary",
                    Surname = "Smith",
                    Phone = "112",
                    Age = "52",

                }, commandType: CommandType.StoredProcedure);


            }
            return Ok();

        }
        [HttpGet]
        public IActionResult DapperResultMapping()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {   // Oluşturduğumuz metod ile Person tablosunda bulunan bütün kayıtları select ederiz.
                //Dapper'ın Query methodunu kullanarak method dbden dönen result set'i model'ime map etmektedir.
                string sql = "";
                sql = @"SELECT * FROM [Person].[Person]";
                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                    var affectedRows = db.Query<Person>(sql);
                }
            }
            return Ok();
        }


    }
}

