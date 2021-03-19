using homework1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace homework1.Data
{
    public class ProductData
    {
        private static volatile ProductData _instance = null;
        private static readonly object padLock = new object();
       

        public List<ProductModel> Product = new List<ProductModel>();
        public static ProductData Instance
        {
            get
            {
                lock (padLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ProductData();
                    }
                }
                return _instance;
            }
        }

        private ProductData()
        {
            FillData();
        }

        private void FillData()
        {
            string data = File.ReadAllText("homework1.json");
            List<ProductModel> tempProducts = JsonConvert.DeserializeObject<List<ProductModel>>(data);

            if (tempProducts != null)
            {
                foreach (var product in tempProducts)
                {
                    Product.Add(new ProductModel { Id = product.Id, Name = product.Name, Price = product.Price });
                }
            }
        }
        public void SaveFile(List<ProductModel> products)
        {
            string result = JsonConvert.SerializeObject(products);
            System.IO.File.WriteAllText(@"homework1.json", result);

        }


    }
}
