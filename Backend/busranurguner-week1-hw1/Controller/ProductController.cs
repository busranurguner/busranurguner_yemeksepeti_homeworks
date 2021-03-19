using homework1.Data;
using homework1.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        
        private readonly ProductData _productData;

        public ProductController()
        {
            _productData = ProductData.Instance;
        }

        [HttpGet]
        public List<ProductModel> Get()
        {
            return _productData.Product;
        }

        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            var item = _productData.Product.FirstOrDefault(c => c.Id == id);
            return item;
        }

        [HttpPost]
        public void Post([FromBody] ProductModel product)
        {


        _productData.Product.Add(product);
        _productData.SaveFile(_productData.Product);
            
          
          

        }

        [HttpDelete("{id}")]
        public List<ProductModel> Delete(int id)
        {
            var data = _productData.Product.FirstOrDefault(c => c.Id == id);
            if(data != null) 
            {
                _productData.Product.Remove(data);
            }
            return _productData.Product;

        }

        [HttpPut]
        public List<ProductModel> Put([FromBody] ProductModel product)
        {
            var data = _productData.Product.FirstOrDefault(c => c.Id == product.Id);
            if (data != null)
            {
                data = product;
            }
            return _productData.Product;

        }


    }
}
