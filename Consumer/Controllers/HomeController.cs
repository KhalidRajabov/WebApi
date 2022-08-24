using Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Consumer.Controllers
{
    public class HomeController : Controller
    {
    string BaseUrl = "http://localhost:3317/";

    public async Task<IActionResult> Index()
    {

            ProductAll productAll;
        

            using (HttpClient client = new HttpClient())
            {
                var api =BaseUrl+ "api/Product/";
                var response = await client.GetAsync(api);
                var result = await response.Content.ReadAsStringAsync();
                productAll = JsonConvert.DeserializeObject<ProductAll>(result);
            }

            return View(productAll);
    }
    public async Task<IActionResult> CreateCategory()
    {
        ProductCreateDto dto = new ProductCreateDto()
        {
            Name = "Iphone9",
            Price = 45,
            DiscountPrice = 30,
            IsActive = true,
            CategoryId = 1
        };
        using (HttpClient client = new HttpClient())
        {
            StringContent content = new StringContent(JsonConvert
                .SerializeObject(dto), Encoding.UTF8, "application/json");
            string endpoint = "http://localhost:5000/api/product";
            var response = await client.PostAsync(endpoint, content);

        }

        return Ok();
    }



}
}