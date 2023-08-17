using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneFinanaceMVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace OneFinanaceMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductController()
        {
            _httpClient = new HttpClient();
        }
        public async Task<IActionResult> GetAllProduct()
        {

            List<GetAllProduct> allProducts = new List<GetAllProduct>();

            using (var response = await _httpClient.GetAsync("https://localhost:7095/api/Product"))
            {
                string data = await response.Content.ReadAsStringAsync();
                allProducts = JsonConvert.DeserializeObject<List<GetAllProduct>>(data);

            }

            return View(allProducts);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {


            DeleteProductVm product = new DeleteProductVm();

            using (var getProductResponse = await _httpClient.GetAsync($"https://localhost:7095/api/Product/{id}"))
            {
                string data = await getProductResponse.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<DeleteProductVm>(data);
            }

            return View(product);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(DeleteProductVm product)
        {

            var response = await _httpClient.DeleteAsync($"https://localhost:7095/api/Product/{product.Id}");
            if (response != null)
            {
                return RedirectToAction("GetAllProduct");
            }
            else
            {
                return View("Error");
            }

        }

        [HttpGet]
        public async Task<ActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDto addProduct)
        {
            int c = Convert.ToInt32(addProduct.CategoryName);//Convert Into String To Interger 
            CategoryEnum cat = (CategoryEnum)c;//Convert Integer Into Enum
            string Category = Enum.GetName(typeof(CategoryEnum), cat); // Enum Convertied into String
            addProduct.CategoryName = Category; // Chnaging Enum value Into corrosponding name
            var content = new StringContent(JsonConvert.SerializeObject(addProduct), Encoding.UTF8, "application/json");



            using (var response = await _httpClient.PostAsync($"https://localhost:7095/api/Product", content))
            {
                var data = response.Content.ReadAsStringAsync().Result; 
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(GetAllProduct));
                }
                else
                {
                    return View("Error");
                }

            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsProduct(int id)
        {
            GetAllProduct product = new GetAllProduct();

            using (var getProductResponse = await _httpClient.GetAsync($"https://localhost:7095/api/Product/{id}"))
            {
                string data = await getProductResponse.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<GetAllProduct>(data);
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {

            var getProductResponse = await _httpClient.GetAsync($"https://localhost:7095/api/Product/{id}");
            if (getProductResponse.IsSuccessStatusCode)
            {
                var productJson = await getProductResponse.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<EditProductDto>(productJson);

                return View(product);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductDto updateProductVm)
        {

            int c = Convert.ToInt32(updateProductVm.CategoryName);
            CategoryEnum cat = (CategoryEnum)c;
            string Category = Enum.GetName(typeof(CategoryEnum), cat);
            updateProductVm.CategoryName = Category;


            var jsonString = JsonConvert.SerializeObject(updateProductVm);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");


            var response = await _httpClient.PutAsync("https://localhost:7095/api/Product/UpdateProduct", content);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(GetAllProduct));
            }
            else
            {
                return View("Error");
            }
        }
    } 

 }
