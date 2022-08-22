using AutoMapper;
using WebApi.Controllers;
using WebApi.Data;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WebApi.Models;

namespace TestProject1
{
    
    [TestClass]
    public class UnitTest1Controller
    {

        [TestMethod]
        public void TestMethod1()
        {
            ProductController newpro = new ProductController();

            

            var test2 = newpro.GetOne(1);
            Assert.IsNotNull(test2);
            
            /*var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = controller.GetAllProducts() as List<Product>;
            Assert.AreEqual(testProducts.Count, result.Count);*/
        }
        /*[TestMethod]
        public void GetProduct_ShouldReturnCorrectProduct()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = controller.GetProduct(1) as OkNegotiatedContentResult<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts[0].Name, result.Content.Name);
        }*/
        /*[TestMethod]
        public void GetProduct_ShouldNotFindProduct()
        {
            var controller = new SimpleProductController(GetTestProducts());

            var result = controller.GetProduct(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }*/

        private List<Product> GetTestProducts()
        {
            var testProducts = new List<Product>();
            testProducts.Add(new Product { Id = 1, Name = "Demo1", 
                Price = 1, DiscountPrice = 0,
            IsActive = true,
            ExpireDate = DateTime.Parse("12/08/2015"),
            CategoryId = 1,
            IsDeleted = false
            });
            testProducts.Add(new Product
            {
                Id = 2,
                Name = "lalala",
                Price = 1,
                DiscountPrice = 0,
                IsActive = true,
                ExpireDate = DateTime.Parse("12/08/2010"),
                CategoryId = 1,
                IsDeleted = false
            });

            /*public int Id { get; set; }
public bool IsDeleted { get; set; }
public DateTime CreatedTime { get; set; }
public DateTime UpdatedTime { get; set; }*/
            /*public string Name { get; set; }
            public decimal Price { get; set; }
            public decimal DiscountPrice { get; set; }
            public bool IsActive { get; set; }
            public DateTime ExpireDate { get; set; }
            public int CategoryId { get; set; }
            public Category Category { get; set; }*/

            return testProducts;
        }
    }
}